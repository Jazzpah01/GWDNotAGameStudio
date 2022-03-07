using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainObject
{
    public GameObject prefab;
    public Vector2 position;
    public Quaternion rotation = Quaternion.identity;
}