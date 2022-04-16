using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDialogueEvent : QuestEvent
{
    public override QuestEventType questEventType => QuestEventType.SetDialogue;

    public List<string> dialogue;

    public override void Execute(SceneContext context)
    {
        throw new System.NotImplementedException();
    }
}
