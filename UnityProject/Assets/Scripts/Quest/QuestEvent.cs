using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestEvent : ScriptableObject
{
    public int questIndex = 0;
    public int questIncrease = 0;
    public bool repeatable = true;
    protected Quest quest;
    [System.NonSerialized] public bool hasExecuted = false;
    public abstract QuestEventType questEventType { get; }
    public abstract void Execute(SceneContext context);
    public void Init(Quest quest)
    {
        this.quest = quest;
    }

    public virtual bool ShouldExecute(SceneContext context)
    {
        if (!repeatable && hasExecuted)
            return false;

        return true;
    }
}
