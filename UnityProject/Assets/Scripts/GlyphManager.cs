using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlyphManager
{
    public static GlyphTime time;
    public static GlyphLandscape landscape;
    public static GlyphBiome biome;

    public static List<Glyph> playerGlyphs = new List<Glyph>();

    public static int timeIndex = -1;
    public static int oldTimeIndex = -1;

    public static GlyphCollection collection;

    public static int GetIndex(Glyph g)
    {
        if (g is GlyphLandscape)
        {
            for (int i = 0; i < collection.landscapes.Count; i++)
            {
                if (g == collection.landscapes[i])
                    return i;
            }
        } else if (g is GlyphBiome)
        {
            for (int i = 0; i < collection.biomes.Count; i++)
            {
                if (g == collection.biomes[i])
                    return i;
            }
        } else if (g is GlyphTime)
        {
            for (int i = 0; i < collection.times.Count; i++)
            {
                if (g == collection.times[i])
                    return i;
            }
        }
        throw new System.Exception("Could not find index from glyph.");
    }


    // TODO: refactor this
    public static Location GetLocation()
    {
        
        int i = collection.landscapes.IndexOf(landscape);

        Location retval = null;

        foreach (Location l in collection.landscapes[i].locations)
        {
            if (l.biome == biome)
                return l;
        }
        

        throw new System.Exception("Cannot find location");
    }
}