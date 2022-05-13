using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveQuestEvent : QuestEvent
{
    public override QuestEventType questEventType => QuestEventType.Move;

    public GameObject moveablePrefab;
    public float speed;
    public Vector2 toPosition;

    public override void Execute(SceneContext context)
    {
        foreach (GameObject go in context.UnorderedEventObjects)
        {
            IMoveable moveable = go.GetComponent<IMoveable>();
            if (moveable != null && moveablePrefab.name == moveable.ID)
            {
                moveable.MoveableSpeed = speed;
                moveable.MoveTo(toPosition, CallBack);
            }
        }
    }

    public override bool ShouldExecute(SceneContext context)
    {
        // Now only NPC, make player too
        return !hasExecuted && context.ContainsNPC(moveablePrefab);
    }
}
