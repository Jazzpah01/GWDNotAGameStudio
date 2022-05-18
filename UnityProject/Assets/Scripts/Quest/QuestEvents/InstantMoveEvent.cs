using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantMoveEvent : QuestEvent
{
    public GameObject moveablePrefab;
    public Vector2 position;

    public override QuestEventType questEventType => QuestEventType.InstantMove;

    public override void Execute(SceneContext context)
    {
        if (questIndex != quest.QuestIndex)
            return;
        foreach (GameObject item in context.UnorderedEventObjects)
        {
            IMoveable moveable = item.GetComponent<IMoveable>();
            if (moveable != null && moveable.ID == moveablePrefab.name.Replace("(Clone)", ""))
            {
                moveable.InstantMove(position);
                CallBack();
            }
        }
    }

    public override bool ShouldExecute(SceneContext context)
    {
        foreach (GameObject item in context.UnorderedEventObjects)
        {
            IMoveable moveable = item.GetComponent<IMoveable>();
            if (moveable != null && moveable.ID == moveablePrefab.name.Replace("(Clone)", ""))
            {
                Debug.Log("Quest instant move!");
                return true;
            }
        }
        return false;
    }
}