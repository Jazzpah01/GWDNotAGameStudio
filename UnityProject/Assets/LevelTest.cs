using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTest : MonoBehaviour
{
    public Biome biome;
    public TimeGlyph time;
    public Landscape landscape;

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
            LevelManager.instance.ChangeScene();
        }
    }
}