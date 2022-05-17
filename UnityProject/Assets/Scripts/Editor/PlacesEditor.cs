using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Places))]
public class PlacesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Places obj = (Places)serializedObject.targetObject;

        if (!obj.initialized)
        {
            if (GUILayout.Button("Initialize"))
            {
                EditorUtility.SetDirty(obj);
                InitializeArray(obj);
            }
        }
        if (obj.initialized && obj.places != null && obj.glyphCollection != null && obj.lengthOfX * obj.lengthOfY * obj.lengthOfZ != obj.places.Length)
        {
            GUILayout.Label("ERROR: Current length of place array does not equal dimensions!");
            if (GUILayout.Button("Resize"))
            {
                RestructureDimensions(obj, obj.lengthOfX, obj.lengthOfY, obj.lengthOfZ);
            }
        }
        if (obj.initialized)
        {
            if (GUILayout.Button("Manual Save"))
            {
                ManualSave(obj);
            }
            if (GUILayout.Button("Log objects"))
            {
                Debug.Log(obj.ToString());
            }
        }
    }

    /// <summary>
    /// Restructure the current data, to fit new size of dimensions, allowing extension of glyphs without corrupting the current data.
    /// </summary>
    /// <param name="xl">New size of x</param>
    /// <param name="yl">New size of y</param>
    /// <param name="zl">New size of z</param>
    public static void RestructureDimensions(Places place, int xl, int yl, int zl)
    {
        GameObject[] newPlaces = new GameObject[xl * yl * zl];

        int lengthOfX = place.glyphCollection.landscapes.Count;
        int lengthOfY = place.glyphCollection.biomes.Count;
        int lengthOfZ = place.glyphCollection.times.Count;

        for (int x = 0; x < place.currentLengthOfX; x++)
        {
            if (x > lengthOfX - 1)
            {
                continue;
            }
            for (int y = 0; y < place.currentLengthOfY; y++)
            {
                if (y > lengthOfY - 1)
                {
                    continue;
                }
                for (int z = 0; z < place.currentLengthOfZ; z++)
                {
                    if (z > lengthOfZ - 1)
                    {
                        continue;
                    }

                    string name = $"Place({x},{y},{z}).prefab";
                    GameObject prefab = place.places[x + y * place.currentLengthOfX + z * place.currentLengthOfX * place.currentLengthOfY];
                    if (prefab == null)
                        continue;

                    GameObject newPrefab = PrefabUtility.SaveAsPrefabAsset(prefab, $"Assets/Resources/Places/{name}");

                    newPlaces[x + y * lengthOfX + z * lengthOfX * lengthOfY] = newPrefab;
                }
            }
        }

        place.places = newPlaces;

        place.currentLengthOfX = lengthOfX;
        place.currentLengthOfY = lengthOfY;
        place.currentLengthOfZ = lengthOfZ;
    }

    /// <summary>
    /// Initialize the data for each of the places. This simply sets the array accoring to amount of glyphs.
    /// </summary>
    public static void InitializeArray(Places place)
    {
        if (place.places != null && place.places.Length != 0)
        {
            throw new System.Exception("Array is initialized while it already exist.");
        }

        place.currentLengthOfX = place.glyphCollection.landscapes.Count;
        place.currentLengthOfY = place.glyphCollection.biomes.Count;
        place.currentLengthOfZ = place.glyphCollection.times.Count;

        place.initialized = true;
        place.places = new GameObject[place.currentLengthOfX * place.currentLengthOfX * place.currentLengthOfX];
    }

    /// <summary>
    /// Save a gameobject and create a prefab. This prefab will be references by this data object.
    /// </summary>
    /// <param name="x">Index of landscape glyph.</param>
    /// <param name="y">Index of biome glyph.</param>
    /// <param name="z">Index of time glyph.</param>
    /// <param name="gameOject">Object to be saves.</param>
    public static void SavePlacePrefab(Places place, int x, int y, int z, GameObject gameOject)
    {
        string name = $"Place({x},{y},{z}).prefab";

        GameObject prefab = Resources.Load<GameObject>($"Places/{name}");
        if (prefab != null)
        {
            Debug.Log("Destroy old prefab!");
            DestroyImmediate(prefab, true);
        }

        int lengthOfX = place.glyphCollection.landscapes.Count;
        int lengthOfY = place.glyphCollection.biomes.Count;
        int lengthOfZ = place.glyphCollection.times.Count;

        Debug.Log("Saving prefab!");

        if (place.path == null || place.path == "")
        {
            string guid = AssetDatabase.CreateFolder("Assets/Data", place.name);
            place.path = AssetDatabase.GUIDToAssetPath(guid);
        }

        GameObject newPrefab = PrefabUtility.SaveAsPrefabAsset(gameOject, $"{place.path}/{name}");
        place.places[x + y * lengthOfX + z * lengthOfX * lengthOfY] = newPrefab;
        EditorUtility.SetDirty(place);
        AssetDatabase.SaveAssets();
    }

    

    /// <summary>
    /// The selected prefab will be saved according to the selected glyph indices.
    /// </summary>
    public static void ManualSave(Places place)
    {
        if (place.glyphCollection == null)
            throw new System.Exception("Glyph Collection is null!");
        if (place.landscape == null)
            throw new System.Exception("Landscape is null!");
        if (place.biome == null)
            throw new System.Exception("Biome is null!");
        if (place.time == null)
            throw new System.Exception("Time is null!");

        GlyphManager.collection = place.glyphCollection;
        int x = place.landscape;
        int y = place.biome;
        int z = place.time;

        place.places[x + y * place.lengthOfX + z * place.lengthOfX * place.lengthOfY] = place.prefab;
        EditorUtility.SetDirty(place);
        AssetDatabase.SaveAssets();
    }
}