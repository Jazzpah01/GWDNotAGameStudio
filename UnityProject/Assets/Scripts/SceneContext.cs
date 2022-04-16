using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContext
{
    /// <summary>
    /// Key: Scene GameObject.
    /// Value: Prefab of the GameObject.
    /// </summary>
    public Dictionary<GameObject, GameObject> EventObjects = new Dictionary<GameObject, GameObject>();
}