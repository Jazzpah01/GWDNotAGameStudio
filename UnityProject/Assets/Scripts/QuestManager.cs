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
        SceneContext context = LevelManager.instance.sceneContext;

        foreach (QuestEvent eventt in questEventsToExecute.ToArray())
        {
            if (eventt.ShouldExecute(context))
            {
                eventt.Execute(context);
            }
            questEventsToExecute.Remove(eventt);
        }
    }

    public static void SubscribeEvent(QuestEvent questEvent)
    {
        if (instance == null)
            throw new System.Exception("There is no QuestManager instance in the scene!");

        if (instance.questEventsToExecute.Contains(questEvent))
            return;

        instance.questEventsToExecute.Add(questEvent);
    }
}
