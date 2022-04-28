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

    public void SpawnGameObject(GameObject prefab, Vector2 position, Vector2 scale)
    {
        GameObject ngo = MonoBehaviour.Instantiate(prefab);
        ngo.transform.position = position;
        ngo.transform.localScale = scale;

        if (!EventObjects.ContainsKey(prefab))
            EventObjects.Add(prefab, new List<GameObject>());

        EventObjects[prefab].Add(ngo);

        NPCController npc = ngo.GetComponent<NPCController>();

        if (npc != null)
        {
            npcs.Add(npc);
        }
    }

    public void RemoveGameObject(GameObject prefab)
    {
        if (EventObjects.ContainsKey(prefab))
        {
            foreach (GameObject go in EventObjects[prefab].ToArray())
            {
                MonoBehaviour.Destroy(go);
            }
            EventObjects[prefab].Clear();
        } else
        {
            NPCController npc = prefab.GetComponentInChildren<NPCController>();
            //Make NPC that didn't spawn in quest removeable
            //foreach (var item in collection)
            //{

            //}
        }
    }
}