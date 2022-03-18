using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public Canvas canvasUI;
    public WaystoneUI waystoneUI;
    public GameObject player;

    private bool isUIopen;

    InteractableUI[] playerGlyphsUI;
    InteractableUI[] waystoneGlyphsUI;
    InteractableUI submitButton;

    public List<Glyph> playerGlyphs;

    public Glyph currentGlyph;

    public bool init = false;


    private void Awake()
    {
        // MonoBehavior singleton pattern
        //if(GameManager.instance == null)
        //{
        //    GameManager.instance = this;
        //} else
        //{
        //    Destroy(this.gameObject);
        //}
        GameManager.instance = this;
        init = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        isUIopen = false;

        //canvasUI = WaystoneUI.instance.transform.parent.GetComponent<Canvas>();
    }

    void DelayedStart()
    {
        playerGlyphs = GlyphManager.playerGlyphs;

        waystoneUI = WaystoneUI.instance;

        // Populate ui
        // TODO



        init = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!init)
        {
            DelayedStart();
        }
        
        waystoneUI.gameObject.SetActive(isUIopen);
        
        if (isUIopen)
        {

        }
    }

    


    public bool getIsUIopen()
    {
        return isUIopen;
    }

    public void setIsUIopen(bool isOpen)
    {
        isUIopen = isOpen;
        Debug.Log("UI open is:   " + isOpen);
    }
}
