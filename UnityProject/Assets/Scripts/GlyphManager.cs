using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlyphManager
{
    public static GlyphTime time;
    public static GlyphLandscape landscape;
    public static GlyphBiome biome;

    public static List<Glyph> playerGlyphs = new List<Glyph>();

    public static List<GlyphLandscape> landscapes;
    public static List<GlyphBiome> biomes;
    public static List<GlyphTime> times;
}