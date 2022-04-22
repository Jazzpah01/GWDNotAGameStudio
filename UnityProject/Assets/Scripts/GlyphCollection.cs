using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Assets/Glyph Collection")]
public class GlyphCollection : ScriptableObject
{
    public List<GlyphLandscape> landscapes;// Glyphs;
    public List<GlyphBiome> biomes;// Glyphs;
    public List<GlyphTime> times;// Glyphs;
}