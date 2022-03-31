using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Places : ScriptableObject
{
    // 3d array of places
    public List<GameObject> places;

    public int lengthOfX;
    public int lengthOfY;
    public int lengthOfZ;

    /// <summary>
    /// Restructure the current data, to fit new size of dimensions.
    /// </summary>
    /// <param name="x">New size of x</param>
    /// <param name="y">New size of y</param>
    /// <param name="z">New size of z</param>
    public void RestructureDimensions(int x, int y, int z)
    {
        throw new System.Exception("not done");
    }

    public void SavePlacePrefab(int x, int y, int z, GameObject gameOject)
    {
        PrefabUtility.SaveAsPrefabAsset(gameOject, $"Assets/Resources/Places/Place({x},{y},{z}).prefab");
        places[x + y * lengthOfX + z * lengthOfX * lengthOfY] = Resources.Load<GameObject>($"Places/Place({x},{y},{z}).prefab");
        AssetDatabase.SaveAssets();
    }

    public GameObject LoadPlacePrefab(int x, int y, int z)
    {
        GameObject retval = places[x + y * lengthOfX + z * lengthOfX * lengthOfY];
        return retval;
    }
}