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
            if (npc.name != null && npc.name != "" && 
                npc.characterName == this.npcPrefab.GetComponent<NPCController>().characterName)
            {
                npc.SetDialogue(dialogue, CallBack);
                NPCController prefabController = npcPrefab.GetComponent<NPCController>();
                npc.defaultVoice = prefabController.defaultVoice;
                return;
            }
        }
        throw new System.Exception($"Could not set dialogue! Prefab: {npcPrefab}. Dialogue: {dialogue}.");
    }

    public override bool ShouldExecute(SceneContext context)
    {
        foreach (NPCController npc in context.npcs)
        {
            if (npc != null && npc.characterName != null && npc.characterName != "" &&
                npc.characterName == this.npcPrefab.GetComponent<NPCController>().characterName)
            {
                return true;
            }
        }
        return false;
    }
}
