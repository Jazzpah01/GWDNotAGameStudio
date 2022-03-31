using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Assets/Glyph Collection")]
public class GlyphCollection : ScriptableObject
{
    public List<GlyphLandscape> landscapeGlyphs;
    public List<GlyphBiome> biomeGlyphs;
    public List<GlyphTime> timeGlyphs;
}