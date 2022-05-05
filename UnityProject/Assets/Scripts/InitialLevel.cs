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

    public List<Quest> initialQuests;

    public static bool gameInitialized = false;
    public static bool firstSceneLoaded = false;

    void Start()
    {
        //GameManager.instance.playerGlyphs = playerGlyphs;

        firstSceneLoaded = false;

        foreach (Glyph glyph in playerGlyphs)
        {
            GlyphManager.playerGlyphs.Add(glyph);
        }

        GlyphManager.collection = glyphCollection;

        GlyphManager.time = timeGlyph;
        GlyphManager.biome = biomeGlyph;
        GlyphManager.landscape = landscapeGlyph;

        GlyphManager.timeIndex = 0;

        gameInitialized = true;

        PlaceManager._places = places;

        foreach (Quest item in initialQuests)
        {
            QuestManager.currentQuests.Add(item);
            item.Init();
        }

        //SceneManager.LoadScene(landscapeGlyph.sceneName);
        LevelManager.ChangeScene();
    }
}