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

    public List<Interactable> interactables = new List<Interactable>();

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

        Interactable interact = ngo.GetComponent<Interactable>();
        Debug.Log("HERE",interact);

        if (interact != null)
        {
            interactables.Add(interact);
        }
    }

    public void AddNPC(NPCController npc)
    {
        if (npcs.Contains(npc))
            return;

        npcs.Add(npc);
    }

    public void AddInteractable(Interactable interactable)
    {
        if (interactables.Contains(interactable))
            return;
        Debug.Log("HERE2", interactable);

        interactables.Add(interactable);
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
            //Make NPC that didn't spawn in quest removeable
            NPCController npc = prefab.GetComponent<NPCController>();
            if (npc != null)
            {
                foreach (NPCController othernpc in npcs.ToArray())
                {
                    if (npc.characterName == othernpc.characterName)
                    {
                        npcs.Remove(othernpc);
                        EventObjects.Remove(othernpc.gameObject);
                    }

                    MonoBehaviour.Destroy(othernpc.gameObject);
                }
            }

            if (prefab == null)
                return;

            Interactable interactable = prefab.GetComponent<Interactable>();
            if (interactable != null)
            {
                foreach (Interactable otherinteractable in interactables.ToArray())
                {
                    if (interactable.ID == otherinteractable.ID)
                    {
                        interactables.Remove(otherinteractable);
                        EventObjects.Remove(otherinteractable.gameObject);
                    }

                    MonoBehaviour.Destroy(otherinteractable.gameObject);
                }
            }
        }
    }
}