using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTest : MonoBehaviour
{
    public GlyphBiome biome;
    public GlyphTime time;
    public GlyphLandscape landscape;

    private static bool init = false;

    private void Awake()
    {
        GlyphManager.biome = biome;
        GlyphManager.time = time;
        GlyphManager.landscape = landscape;
    }

    private void Start()
    {
        if (!init)
        {
            init = true;
            LevelManager.ChangeScene();
        }
    }
}