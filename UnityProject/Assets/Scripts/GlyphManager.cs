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


    // TODO: refactor this
    public static Location GetLocation(GlyphLandscape x, GlyphBiome y)
    {
        int i = landscapes.IndexOf(x);

        Location retval = null;

        foreach (GlyphBiome b in biomes)
        {
            foreach (Location l in landscapes[i].locations)
            {
                if (l.biome == b)
                    return l;
            }
        }

        throw new System.Exception("Cannot find location");
    }
}