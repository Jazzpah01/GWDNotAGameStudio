using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public Canvas canvasUI;

    private bool isUIopen;


    private void Awake()
    {
        // MonoBehavior singleton pattern
        if(GameManager.instance == null)
        {
            GameManager.instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isUIopen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUIopen)
        {
            canvasUI.gameObject.SetActive(true);
        } else
        {
            canvasUI.gameObject.SetActive(false);
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
