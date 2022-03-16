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

        // Delegate button presses
        playerGlyphsUI = waystoneUI.playerGlyphs.GetComponentsInChildren<InteractableUI>();

        for (int i = 0; i < playerGlyphsUI.Length; i++)
        {
            int index = i;
            playerGlyphsUI[i].OnClicked += delegate { SelectGlyph(playerGlyphs[index]); };
        }

        waystoneGlyphsUI = waystoneUI.waystoneGlyphs.GetComponentsInChildren<InteractableUI>();

        waystoneGlyphsUI[0].OnClicked += delegate { SetGlyph(currentGlyph); };
        waystoneGlyphsUI[1].OnClicked += delegate { SetGlyph(currentGlyph); };

        submitButton = waystoneUI.submitButton.GetComponent<InteractableUI>();
        submitButton.OnClicked += delegate { Submit(); };

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

    public void SelectGlyph(Glyph g)
    {
        Debug.Log("Glyph selected!");
        currentGlyph = g;
        waystoneUI.SelectGlyph(g);
    }

    public void SetGlyph(Glyph g)
    {
        Debug.Log("Glyph set!");
        waystoneUI.SetGlyph(g);
        if (g is GlyphLandscape)
        {
            GlyphManager.landscape = (GlyphLandscape)g;
        } else if (g is GlyphBiome)
        {
            GlyphManager.biome = (GlyphBiome)g;
        } else if (g is GlyphTime)
        {
            GlyphManager.time = (GlyphTime)g;
        }
    }

    public void Submit()
    {
        if (GlyphManager.biome == null || GlyphManager.landscape == null)
        {
            Debug.Log("Invalid glyphs");
            return;
        }
            

        LevelManager.instance.ChangeScene();
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
