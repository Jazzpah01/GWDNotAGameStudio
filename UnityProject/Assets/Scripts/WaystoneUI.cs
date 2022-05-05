using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaystoneUI : MonoBehaviour
{


    public GameObject playerGlyphs;
    public GameObject biomeUI;
    public GameObject landscapeUI;
    public GameObject waystoneGlyphs;
    public GameObject submitButton;

    public SpriteRenderer landscapeSlot;
    public SpriteRenderer biomeSlot;

    public GameObject playerItemPrefab;

    public static WaystoneUI instance;

    private Glyph currentGlyph;
    //private List<InteractableUI> playerItems;
    private List<(InteractableUI,int)> UI_biomeGlyphs;
    private List<(InteractableUI,int)> UI_landscapeGlyphs;

    private InteractableUI oldToggled;

    [Header("FMOD")]
    public FMODUnity.EventReference interact;
    public FMODUnity.EventReference gainGlyph;
    public FMODUnity.EventReference transition;
    public FMODUnity.StudioEventEmitter hoverEmitter;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CleanupMenuItems();
        PopulateMenuItems();

        //landscapeSlot.OnClicked += delegate { SetGlyph(currentGlyph); };
        //biomeSlot.OnClicked += delegate { SetGlyph(currentGlyph); };

        //landscapeSlot.Interactable = false;
        //biomeSlot.Interactable = false;

        submitButton.GetComponent<InteractableUI>().OnClicked += delegate { Submit(); };

        //SetGlyph(GlyphManager.landscape);
        //SetGlyph(GlyphManager.biome);
    }

    public void Resetup()
    {
        CleanupMenuItems();
        PopulateMenuItems();
    }

    /*
    void CleanupMenuItems()
    {
        if (playerItems == null)
            return;

        foreach (InteractableUI item in playerItems.ToArray())
        {
            item.OnClicked = null;
            Destroy(item.gameObject);
        }

        playerItems.Clear();
    }
    */
    void CleanupMenuItems()
    {
        if (UI_biomeGlyphs == null && UI_landscapeGlyphs == null)
            return;

        foreach ((InteractableUI,int) item in UI_biomeGlyphs.ToArray())
        {
            item.Item1.OnClicked = null;
            Destroy(item.Item1.gameObject);
        }

        foreach ((InteractableUI, int) item in UI_landscapeGlyphs.ToArray())
        {
            item.Item1.OnClicked = null;
            Destroy(item.Item1.gameObject);
        }
        
        UI_biomeGlyphs.Clear();
        UI_landscapeGlyphs.Clear();
    }

    /*
    void PopulateMenuItems()
    {
        playerItems = new List<InteractableUI>();

        // Create UI elements for player items
        for (int i = 0; i < GlyphManager.playerGlyphs.Count; i++)
        {
            GameObject go = Instantiate(playerItemPrefab, playerGlyphs.transform);
            InteractableUI iui = go.GetComponent<InteractableUI>();
            iui.Sprite = GlyphManager.playerGlyphs[i].icon;
            playerItems.Add(iui); ;
        }

        // Delegate button presses for player items
        for (int i = 0; i < playerItems.Count; i++)
        {
            int index = i;
            playerItems[i].OnClicked += delegate {
                SelectGlyph(GlyphManager.playerGlyphs[index]);
                playerItems[index].Toggled = true;
                if (oldToggled != null)
                    oldToggled.Toggled = false;
                oldToggled = playerItems[index];
            };
        }
    }
    */

    void PopulateMenuItems()
    {
        //playerItems = new List<InteractableUI>();
        UI_biomeGlyphs = new List<(InteractableUI,int)>();
        UI_landscapeGlyphs = new List<(InteractableUI, int)>();

        // Create UI elements for player items
        for (int i = 0; i < GlyphManager.playerGlyphs.Count; i++)
        {
            //GameObject go = Instantiate(playerItemPrefab, playerGlyphs.transform);
            //InteractableUI iui = go.GetComponent<InteractableUI>();
            //iui.Sprite = GlyphManager.playerGlyphs[i].icon;
            //playerItems.Add(iui);
            //if (GlyphManager.playerGlyphs[i] is GlyphBiome) UI_biomeGlyphs.Add((iui, i));
            //if (GlyphManager.playerGlyphs[i] is GlyphLandscape) UI_landscapeGlyphs.Add((iui, i));

            if (GlyphManager.playerGlyphs[i] is GlyphBiome)
            {
                GameObject go = Instantiate(playerItemPrefab, biomeUI.transform);
                InteractableUI iui = go.GetComponent<InteractableUI>();
                iui.Sprite = GlyphManager.playerGlyphs[i].icon;
                UI_biomeGlyphs.Add((iui, i));
            }

            if (GlyphManager.playerGlyphs[i] is GlyphLandscape)
            {
                GameObject go = Instantiate(playerItemPrefab, landscapeUI.transform);
                InteractableUI iui = go.GetComponent<InteractableUI>();
                iui.Sprite = GlyphManager.playerGlyphs[i].icon;
                UI_landscapeGlyphs.Add((iui, i));
            }
        }

        // Delegate button presses for player items
        //for (int i = 0; i < playerItems.Count; i++)
        //{
        //    int index = i;
        //    playerItems[i].OnClicked += delegate {
        //        SelectGlyph(GlyphManager.playerGlyphs[playerItems[index].]);
        //        playerItems[index].Toggled = true;
        //        if (oldToggled != null)
        //            oldToggled.Toggled = false;
        //        oldToggled = playerItems[index];
        //    };
        //}

        for (int i = 0; i < UI_landscapeGlyphs.Count; i++)
        {
            int j = i;
            UI_landscapeGlyphs[i].Item1.OnClicked += delegate
            {
                SelectGlyph(GlyphManager.playerGlyphs[UI_landscapeGlyphs[j].Item2]);
                UI_landscapeGlyphs[j].Item1.Toggled = true;
                if (oldToggled != null)
                    oldToggled.Toggled = false;
                oldToggled = UI_landscapeGlyphs[j].Item1;
            };
        }

        for (int i = 0; i < UI_biomeGlyphs.Count; i++)
        {
            int j = i;
            UI_biomeGlyphs[i].Item1.OnClicked += delegate
            {
                SelectGlyph(GlyphManager.playerGlyphs[UI_biomeGlyphs[j].Item2]);
                UI_biomeGlyphs[j].Item1.Toggled = true;
                if (oldToggled != null)
                    oldToggled.Toggled = false;
                oldToggled = UI_biomeGlyphs[j].Item1;
            };
        }
    }


    public void SelectGlyph(Glyph g)
    {
        currentGlyph = g;
        if (g is GlyphBiome)
        {
            GlyphManager.biome = (GlyphBiome)g;
            SetGlyph(g);
            FMODUnity.RuntimeManager.PlayOneShot(interact);
        }
        else if (g is GlyphLandscape)
        {
            GlyphManager.landscape = (GlyphLandscape)g;
            SetGlyph(g);
            FMODUnity.RuntimeManager.PlayOneShot(interact);
        } else
        {
        }
    }

    public void SetGlyph(Glyph g)
    {
        if (oldToggled != null)
        {
            oldToggled.Toggled = false;
            oldToggled = null;
        }
        if (g is GlyphBiome)
        {
            biomeSlot.sprite = g.icon;
        } else if (g is GlyphLandscape)
        {
            landscapeSlot.sprite = g.icon;
        }
    }

    public void Submit()
    {
        if (GlyphManager.biome == null || GlyphManager.landscape == null)
        {
            Debug.Log("Invalid glyphs");
            return;
        }

        if (biomeSlot.sprite == null || landscapeSlot.sprite == null)
            return;

        print("Submit! ChangeScene in WaystoneUI");
        LevelManager.ChangeScene();
    }


}