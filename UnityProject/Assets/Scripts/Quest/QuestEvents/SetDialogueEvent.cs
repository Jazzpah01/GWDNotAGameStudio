using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDialogueEvent : QuestEvent
{
    public override QuestEventType questEventType => QuestEventType.SetDialogue;

    public GameObject npcPrefab;
    public Dialogue dialogue;

    public override void Execute(SceneContext context)
    {
        foreach (NPCController npc in context.npcs)
        {
            Debug.Log("Npc exists!");
            if (npc.name != null && npc.name != "" && 
                npc.characterName == this.npcPrefab.GetComponent<NPCController>().characterName)
            {
                npc.SetDialogue(dialogue, CallBack);
                return;
            }
        }
        throw new System.Exception($"Could not set dialogue! Prefab: {npcPrefab}. Dialogue: {dialogue}.");
    }

    public override bool ShouldExecute(SceneContext context)
    {
        foreach (NPCController npc in context.npcs)
        {
            Debug.Log($"{npc.characterName}---{this.npcPrefab.GetComponent<NPCController>().characterName}");
            if (npc.characterName != null && npc.characterName != "" &&
                npc.characterName == this.npcPrefab.GetComponent<NPCController>().characterName)
            {
                Debug.Log("Yes dialogue");
                return true;
            }
        }
        Debug.Log($"No dialogue. NPC count: {context.npcs.Count}");
        return false;
    }
}
