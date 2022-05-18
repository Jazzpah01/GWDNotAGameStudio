using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartMenuManager : MonoBehaviour
{

    public Image blackFader;
    public Image studioTitle;
    public GameObject studioTitlePanel;
    public GameObject controlsPanel;
    public GameObject creditsPanel;

    public VideoPlayer vp;
    
    private bool isStarting = false;
    private float timer = 0f;
    private bool videoHasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {

        var btnStart = GameObject.Find("btnStart").GetComponent<Button>();
        btnStart.onClick.AddListener(StartGame);
        var btnControls = GameObject.Find("btnControls").GetComponent<Button>();
        btnControls.onClick.AddListener(ShowControls);
        var btnCredits = GameObject.Find("btnCredits").GetComponent<Button>();
        btnCredits.onClick.AddListener(ShowCredits);
        var btnExit = GameObject.Find("btnExit").GetComponent<Button>();
        btnExit.onClick.AddListener(ExitGame);

        vp.loopPointReached += EndReached;
    }


    // Update is called once per frame
    void Update()
    {
    
        if (studioTitlePanel.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer > 4f) studioTitlePanel.SetActive(false);
        }


        if (isStarting)
        {
            timer += Time.deltaTime;

            Color col = blackFader.color;
            col.a += 0.5f * Time.deltaTime;
            blackFader.color = col;
            //blackFader.color.a += 0.5 * Time.deltaTime;

            if (timer > 3f && !videoHasPlayed)
            {
                PlayVideo();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (controlsPanel.activeSelf || creditsPanel.activeSelf)
            {
                controlsPanel.SetActive(false);
                creditsPanel.SetActive(false);
            }
        }
    }

    void EndReached(VideoPlayer v)
    {
        Debug.Log("StartMenuManager: Video End Reached!");
        SceneManager.LoadScene("InitialScene");
    }

    void PlayVideo()
    {
        if (!vp.isPlaying)
        {
            vp.Play();
            videoHasPlayed = true;
            
        }
    }

    void StartGame()
    {
        Debug.Log("Starting Game!");
        isStarting = true;
        timer = 0f;
    }

    void ShowControls()
    {
        controlsPanel.SetActive(true);
        Debug.Log("Showing Controls!");
    }

    void ShowCredits()
    {
        creditsPanel.SetActive(true);
        Debug.Log("Showing Credits!");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
