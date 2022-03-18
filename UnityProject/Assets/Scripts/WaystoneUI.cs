using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaystoneUI : MonoBehaviour
{
    public GameObject playerGlyphs;
    public GameObject waystoneGlyphs;
    public GameObject submitButton;

    public InteractableUI landscapeSlot;
    public InteractableUI biomeSlot;

    public GameObject playerItemPrefab;

    public static WaystoneUI instance;

    private Glyph currentGlyph;
    private List<InteractableUI> playerItems;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PopulateMenuItems();

        // Delegate button presses
        for (int i = 0; i < playerItems.Count; i++)
        {
            int index = i;
            playerItems[i].OnClicked += delegate { SelectGlyph(GlyphManager.playerGlyphs[index]); };
        }

        landscapeSlot.OnClicked += delegate { SetGlyph(currentGlyph); };
        biomeSlot.OnClicked += delegate { SetGlyph(currentGlyph); };

        submitButton.GetComponent<InteractableUI>().OnClicked += delegate { Submit(); };
    }

    void PopulateMenuItems()
    {
        playerItems = new List<InteractableUI>();

        for (int i = 0; i < GlyphManager.playerGlyphs.Count; i++)
        {
            GameObject go = Instantiate(playerItemPrefab, playerGlyphs.transform);
            playerItems.Add(go.GetComponent<InteractableUI>());
        }
    }

    public void SelectGlyph(Glyph g)
    {
        currentGlyph = g;
        if (g is GlyphBiome)
        {
            GlyphManager.biome = (GlyphBiome)g;
            landscapeSlot.Interactable = false;
            biomeSlot.Interactable = true;
        }
        else if (g is GlyphLandscape)
        {
            GlyphManager.landscape = (GlyphLandscape)g;
            biomeSlot.Interactable = false;
            landscapeSlot.Interactable = true;
        } else
        {
            landscapeSlot.Interactable = true;
            biomeSlot.Interactable = true;
        }
    }

    public void SetGlyph(Glyph g)
    {
        if (g is GlyphBiome)
        {
            biomeSlot.Sprite = g.icon;
        } else if (g is GlyphLandscape)
        {
            landscapeSlot.Sprite = g.icon;
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

    //public void SelectGlyph(Glyph g)
    //{
    //    Debug.Log("Glyph selected!");
    //    currentGlyph = g;
    //    waystoneUI.SelectGlyph(g);
    //}

    //public void SetGlyph(Glyph g)
    //{
    //    Debug.Log("Glyph set!");
    //    waystoneUI.SetGlyph(g);
    //    if (g is GlyphLandscape)
    //    {
    //        GlyphManager.landscape = (GlyphLandscape)g;
    //    }
    //    else if (g is GlyphBiome)
    //    {
    //        GlyphManager.biome = (GlyphBiome)g;
    //    }
    //    else if (g is GlyphTime)
    //    {
    //        GlyphManager.time = (GlyphTime)g;
    //    }
    //}


}