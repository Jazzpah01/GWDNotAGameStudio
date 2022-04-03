using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialLevel : MonoBehaviour
{
    public GlyphTime timeGlyph;
    public GlyphBiome biomeGlyph;
    public GlyphLandscape landscapeGlyph;

    public List<GlyphLandscape> landscapes;
    public List<GlyphBiome> biomes;
    public List<GlyphTime> times;

    public GlyphCollection glyphCollection;

    public List<Glyph> playerGlyphs;

    public Places places;

    public static bool gameInitialized = false;

    void Start()
    {
        //GameManager.instance.playerGlyphs = playerGlyphs;

        foreach (Glyph glyph in playerGlyphs)
        {
            GlyphManager.playerGlyphs.Add(glyph);
        }

        GlyphManager.collection = glyphCollection;

        GlyphManager.time = timeGlyph;
        GlyphManager.biome = biomeGlyph;
        GlyphManager.landscape = landscapeGlyph;

        gameInitialized = true;

        PlaceManager._places = places;

        SceneManager.LoadScene(landscapeGlyph.sceneName);
    }
}