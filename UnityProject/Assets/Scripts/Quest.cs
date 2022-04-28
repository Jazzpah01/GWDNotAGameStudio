using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Assets/Quest")]
public class Quest : ScriptableObject
{
    [HideInInspector] public bool customEditing = true;
    private int questIndex = 0;

    public int QuestIndex
    {
        get => questIndex;
        set
        {
            float delta = value - questIndex;

            int i = 0;
            for (int j = 0; j < questEvents.Count; j++)
            {
                i += questEvents[j].questIncrease;
                if (questEvents[j].questIndex > value || questEvents[j].questIndex <= questIndex)
                    continue;

                if (questEvents[j].questIndex >= questIndex && 
                    questEvents[j].ShouldExecute(LevelManager.instance.sceneContext))
                {
                    QuestManager.SubscribeEvent(questEvents[j]);
                }
            }

            questIndex = value;
        }
    }

    public void Init()
    {
        questIndex = 0;
        foreach (QuestEvent e in questEvents)
        {
            e.Init(this);
        }
    }

    public List<QuestEvent> questEvents = new List<QuestEvent>();
}
