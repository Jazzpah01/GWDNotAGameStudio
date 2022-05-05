using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static List<Quest> quests;

    public static int currentQuestIndex;

    public static List<Quest> currentQuests = new List<Quest>();

    private static QuestManager instance;

    private List<QuestEvent> questEventsToExecute = new List<QuestEvent>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        ExecuteEvents();
    }

    public static void SubscribeEvent(QuestEvent questEvent)
    {
        if (instance == null)
            throw new System.Exception("There is no QuestManager instance in the scene!");

        if (instance.questEventsToExecute.Contains(questEvent))
            return;

        instance.questEventsToExecute.Add(questEvent);
    }

    public static void ExecuteEvents()
    {
        if (instance.questEventsToExecute.Count <= 0)
            return;

        SceneContext context = LevelManager.instance.sceneContext;

        int count = instance.questEventsToExecute.Count;

        for (int i = 0; i < count; i++)
        {
            if (instance.questEventsToExecute[i].ShouldExecute(context))
            {
                instance.questEventsToExecute[i].Execute(context);
            }
        }

        instance.questEventsToExecute.RemoveRange(0, count);
    }
}
