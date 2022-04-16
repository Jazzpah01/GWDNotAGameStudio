using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialLevel : MonoBehaviour
{
    // Initial place to load:
    public GlyphTime timeGlyph;
    public GlyphBiome biomeGlyph;
    public GlyphLandscape landscapeGlyph;

    public GlyphCollection glyphCollection;

    public List<Glyph> playerGlyphs;

    public Places places;

    public Quest initialQuest;

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

        QuestManager.currentQuests.Add(initialQuest);

        SceneManager.LoadScene(landscapeGlyph.sceneName);
    }
}