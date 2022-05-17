using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// The object that holds data about the places. A coordinate on x,y,z will defines a unique place.
/// A place is a GameObject prefab which will be spawned into the game.
/// </summary>
[CreateAssetMenu(menuName ="Assets/Places")]
public class Places : ScriptableObject
{
    // 3d array of places
    [HideInInspector]public GameObject[] places;

    [HideInInspector]public int currentLengthOfX;
    [HideInInspector]public int currentLengthOfY;
    [HideInInspector]public int currentLengthOfZ;

    [HideInInspector] public string path;

    public GlyphCollection glyphCollection;

    [Header("Manual Save")]
    public int landscape;
    public int biome;
    public int time;
    public GameObject prefab;

    [HideInInspector]public bool initialized = false;

    public int lengthOfX => glyphCollection.landscapes.Count;
    public int lengthOfY => glyphCollection.biomes.Count;
    public int lengthOfZ => glyphCollection.times.Count;


    /// <summary>
    /// Returns a prefab given the coordinates of the place.
    /// </summary>
    /// <param name="x">Index of landscape glyph.</param>
    /// <param name="y">Index of biome glyph.</param>
    /// <param name="z">Index of time glyph.</param>
    /// <returns></returns>
    public GameObject LoadPlacePrefab(int x, int y, int z)
    {
        int lengthOfX = glyphCollection.landscapes.Count;
        int lengthOfY = glyphCollection.biomes.Count;
        int lengthOfZ = glyphCollection.times.Count;

        GameObject retval = places[x + y * lengthOfX + z * lengthOfX * lengthOfY];
        return retval;
    }

    public override string ToString()
    {
        string retval = "Length: " + places.Length;

        foreach (GameObject item in places)
        {
            if (item == null)
                continue;

            retval += "\r\n";
            retval += item.name;
        }

        return retval;
    }
}