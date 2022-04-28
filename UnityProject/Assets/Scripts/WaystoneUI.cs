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

    private InteractableUI oldToggled;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CleanupMenuItems();
        PopulateMenuItems();

        landscapeSlot.OnClicked += delegate { SetGlyph(currentGlyph); };
        biomeSlot.OnClicked += delegate { SetGlyph(currentGlyph); };

        landscapeSlot.Interactable = false;
        biomeSlot.Interactable = false;

        submitButton.GetComponent<InteractableUI>().OnClicked += delegate { Submit(); };

        SetGlyph(GlyphManager.landscape);
        SetGlyph(GlyphManager.biome);
    }

    public void Resetup()
    {
        CleanupMenuItems();
        PopulateMenuItems();
    }

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

    public void SelectGlyph(Glyph g)
    {
        currentGlyph = g;
        if (g is GlyphBiome)
        {
            GlyphManager.biome = (GlyphBiome)g;
            landscapeSlot.Interactable = false;
            biomeSlot.Interactable = true;
            SetGlyph(g);
        }
        else if (g is GlyphLandscape)
        {
            GlyphManager.landscape = (GlyphLandscape)g;
            biomeSlot.Interactable = false;
            landscapeSlot.Interactable = true;
            SetGlyph(g);
        } else
        {
            landscapeSlot.Interactable = true;
            biomeSlot.Interactable = true;
        }
    }

    public void SetGlyph(Glyph g)
    {
        if (oldToggled != null)
        {
            oldToggled.Toggled = false;
            oldToggled = null;
        }
        biomeSlot.Interactable = false;
        landscapeSlot.Interactable = false;
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


}