using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogueEvent : QuestEvent
{
    public Dialogue dialogue;
    public GameObject npcPrefab;

    public override QuestEventType questEventType => QuestEventType.StartDialogue;

    public override void Execute(SceneContext context)
    {
        // Get npc from scene. If npcPrefab is null, then npc is null
        NPCController npc = (npcPrefab == null) ? null : context.GetNPC(npcPrefab);

        DialogueUI.instance.SetDialogue(npc, dialogue, CallBack);
        DialogueUI.instance.StartDialogue();
    }

    public override bool ShouldExecute(SceneContext context)
    {
        if (!hasExecuted && (npcPrefab == null || context.ContainsNPC(npcPrefab)))
        {
            return true;
        } else
        {
            return false;
        }
    }
}