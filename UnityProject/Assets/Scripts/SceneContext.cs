using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContext
{
    /// <summary>
    /// Key: Prefab.
    /// Value: List of game objects of the prefab.
    /// </summary>
    public Dictionary<GameObject, List<GameObject>> EventObjects = new Dictionary<GameObject, List<GameObject>>();

    public List<NPCController> npcs = new List<NPCController>();
}