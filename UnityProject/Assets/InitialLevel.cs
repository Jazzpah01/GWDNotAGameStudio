using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialLevel : MonoBehaviour
{
    public TimeGlyph timeGlyph;
    public Biome biomeGlyph;
    public Landscape landscapeGlyph;

    private void Awake()
    {
        GlyphManager.time = timeGlyph;
        GlyphManager.biome = biomeGlyph;
        GlyphManager.landscape = landscapeGlyph;

        SceneManager.LoadScene(landscapeGlyph.sceneName);
    }
}