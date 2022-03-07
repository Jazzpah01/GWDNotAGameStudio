using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainObject
{
    public GameObject prefab;
    public Vector2 offset = Vector2.zero;
    [Range(0.001f, 1)]
    public float weight = 1;
}