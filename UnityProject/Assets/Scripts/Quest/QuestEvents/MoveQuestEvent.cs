using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveQuestEvent : QuestEvent
{
    public override QuestEventType questEventType => throw new System.NotImplementedException();

    public GameObject moveablePrefab;
    public float speed;
    public Vector2 toPosition;

    public override void Execute(SceneContext context)
    {
        
    }

    public override bool ShouldExecute(SceneContext context)
    {
        throw new System.NotImplementedException();
    }
}
