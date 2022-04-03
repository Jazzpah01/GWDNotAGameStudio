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
    /// Restructure the current data, to fit new size of dimensions, allowing extension of glyphs without corrupting the current data.
    /// </summary>
    /// <param name="xl">New size of x</param>
    /// <param name="yl">New size of y</param>
    /// <param name="zl">New size of z</param>
    public void RestructureDimensions(int xl, int yl, int zl)
    {
        GameObject[] newPlaces = new GameObject[xl * yl * zl];

        int lengthOfX = glyphCollection.landscapes.Count;
        int lengthOfY = glyphCollection.biomes.Count;
        int lengthOfZ = glyphCollection.times.Count;

        for (int x = 0; x < currentLengthOfX; x++)
        {
            if (x > lengthOfX - 1)
            {
                continue;
            }
            for (int y = 0; y < currentLengthOfY; y++)
            {
                if (y > lengthOfY - 1)
                {
                    continue;
                }
                for (int z = 0; z < currentLengthOfZ; z++)
                {
                    if (z > lengthOfZ - 1)
                    {
                        continue;
                    }

                    string name = $"Place({x},{y},{z}).prefab";
                    GameObject prefab = places[x + y * currentLengthOfX + z * currentLengthOfX * currentLengthOfY];
                    if (prefab == null)
                        continue;

                    GameObject newPrefab = PrefabUtility.SaveAsPrefabAsset(prefab, $"Assets/Resources/Places/{name}");

                    newPlaces[x + y * lengthOfX + z * lengthOfX * lengthOfY] = newPrefab;
                }
            }
        }

        places = newPlaces;

        currentLengthOfX = lengthOfX;
        currentLengthOfY = lengthOfY;
        currentLengthOfZ = lengthOfZ;
    }

    /// <summary>
    /// Initialize the data for each of the places. This simply sets the array accoring to amount of glyphs.
    /// </summary>
    public void InitializeArray()
    {
        if (places != null && places.Length != 0)
        {
            throw new System.Exception("Array is initialized while it already exist.");
        }

        currentLengthOfX = glyphCollection.landscapes.Count;
        currentLengthOfY = glyphCollection.biomes.Count;
        currentLengthOfZ = glyphCollection.times.Count;

        initialized = true;
        places = new GameObject[currentLengthOfX * currentLengthOfX * currentLengthOfX];
    }

    /// <summary>
    /// Save a gameobject and create a prefab. This prefab will be references by this data object.
    /// </summary>
    /// <param name="x">Index of landscape glyph.</param>
    /// <param name="y">Index of biome glyph.</param>
    /// <param name="z">Index of time glyph.</param>
    /// <param name="gameOject">Object to be saves.</param>
    public void SavePlacePrefab(int x, int y, int z, GameObject gameOject)
    {
        string name = $"Place({x},{y},{z}).prefab";

        GameObject prefab = Resources.Load<GameObject>($"Places/{name}");
        if (prefab != null)
        {
            Debug.Log("Destroy old prefab!");
            DestroyImmediate(prefab, true);
        }

        int lengthOfX = glyphCollection.landscapes.Count;
        int lengthOfY = glyphCollection.biomes.Count;
        int lengthOfZ = glyphCollection.times.Count;

        Debug.Log("Saving prefab!");

        if (path == null || path == "")
        {
            string guid = AssetDatabase.CreateFolder("Assets/Data", this.name);
            path = AssetDatabase.GUIDToAssetPath(guid);
        }

        GameObject newPrefab = PrefabUtility.SaveAsPrefabAsset(gameOject, $"{path}/{name}");
        places[x + y * lengthOfX + z * lengthOfX * lengthOfY] = newPrefab;
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }

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

    /// <summary>
    /// The selected prefab will be saved according to the selected glyph indices.
    /// </summary>
    public void ManualSave()
    {
        if (glyphCollection == null)
            throw new System.Exception("Glyph Collection is null!");
        if (landscape == null)
            throw new System.Exception("Landscape is null!");
        if (biome == null)
            throw new System.Exception("Biome is null!");
        if (time == null)
            throw new System.Exception("Time is null!");

        GlyphManager.collection = glyphCollection;
        int x = landscape;
        int y = biome;
        int z = time;

        places[x + y * lengthOfX + z * lengthOfX * lengthOfY] = prefab;
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
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