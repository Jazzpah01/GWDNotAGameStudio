using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialLevel : MonoBehaviour
{
    public GlyphTime timeGlyph;
    public GlyphBiome biomeGlyph;
    public GlyphLandscape landscapeGlyph;

    public List<Glyph> playerGlyphs;

    void Start()
    {
        //GameManager.instance.playerGlyphs = playerGlyphs;

        foreach (Glyph glyph in playerGlyphs)
        {
            GlyphManager.playerGlyphs.Add(glyph);
        }

        GlyphManager.time = timeGlyph;
        GlyphManager.biome = biomeGlyph;
        GlyphManager.landscape = landscapeGlyph;

        SceneManager.LoadScene(landscapeGlyph.sceneName);
    }
}