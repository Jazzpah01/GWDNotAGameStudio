using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Assets/Quest")]
public class Quest : ScriptableObject
{

    // list of quest events

    // execute quest events on a specific trigger based on scene objects

    public string guid;
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
                if (i > value)
                    break;

                i += questEvents[j].questIncrease;

                if (i - questEvents[j].questIncrease >= questIndex && 
                    questEvents[j].ShouldExecute(LevelManager.instance.sceneContext))
                {
                    questEvents[j].Execute(LevelManager.instance.sceneContext);
                    questEvents[j].hasExecuted = true;
                }
            }

            questIndex = value;
        }
    }

    public List<QuestEvent> questEvents = new List<QuestEvent>();
}
