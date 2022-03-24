using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlyphManager
{
<<<<<<< HEAD
    public static Time time;
    public static Landscape landscape;
    public static Biome biome;
=======
    public static GlyphTime time;
    public static GlyphLandscape landscape;
    public static GlyphBiome biome;

    public static List<Glyph> playerGlyphs = new List<Glyph>();

    public static int timeIndex = -1;

    public static List<GlyphLandscape> landscapes;
    public static List<GlyphBiome> biomes;
    public static List<GlyphTime> times;


    // TODO: refactor this
    public static Location GetLocation()
    {
        
        int i = landscapes.IndexOf(landscape);

        Location retval = null;

        foreach (Location l in landscapes[i].locations)
        {
            if (l.biome == biome)
                return l;
        }
        

        throw new System.Exception("Cannot find location");
    }
>>>>>>> develop
}