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

    public static WaystoneUI instance;

    private void Awake()
    {
        instance = this;
    }

    public void SelectGlyph(Glyph g)
    {
        if (g is GlyphBiome)
        {
            landscapeSlot.Interactable = false;
            biomeSlot.Interactable = true;
        }
        else if (g is GlyphLandscape)
        {
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
}