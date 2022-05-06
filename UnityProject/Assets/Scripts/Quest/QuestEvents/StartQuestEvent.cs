using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartQuestEvent : QuestEvent
{

    public Quest questToInitiate;
    public override QuestEventType questEventType => QuestEventType.StartQuest;

    public override void Execute(SceneContext context)
    {
        if (!QuestManager.currentQuests.Contains(questToInitiate))
        {
            QuestManager.currentQuests.Add(questToInitiate);
            questToInitiate.Init();

            for (int i = 0; i < questToInitiate.questEvents.Count; i++)
            {
                QuestEvent questEvent = questToInitiate.questEvents[i];

                if (questEvent.questIndex != questToInitiate.QuestIndex)
                    continue;

                QuestManager.SubscribeEvent(questEvent);
            }

            CallBack();
            Debug.Log("Starting new quest");
        }
    }

    public override bool ShouldExecute(SceneContext context)
    {
        if (!QuestManager.currentQuests.Contains(questToInitiate))
        {
            return true;
        } else
        {
            return false;
        }
    }
}