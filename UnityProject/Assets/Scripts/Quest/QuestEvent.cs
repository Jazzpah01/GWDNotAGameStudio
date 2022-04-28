using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestEvent : ScriptableObject, IComparable<QuestEvent>
{
    public int questIndex = 0;
    public int questIncrease = 0;
    //public bool repeatable = true;
    protected Quest quest;
    [System.NonSerialized] public bool hasExecuted = false;
    public abstract QuestEventType questEventType { get; }
    public abstract void Execute(SceneContext context);
    public void Init(Quest quest)
    {
        this.quest = quest;
        hasExecuted = false;
    }

    public abstract bool ShouldExecute(SceneContext context);

    public void LoadQuest()
    {
        // There is a bug, should have quest manager
    }

    public virtual void CallBack() {
        if (!hasExecuted)
        {
            hasExecuted = true;
            quest.QuestIndex += questIncrease;
        }
    }

    public int CompareTo(QuestEvent y)
    {
        QuestEvent x = this;

        if (x.questIndex < y.questIndex)
            return -1;
        if (x.questIndex > y.questIndex)
            return 1;

        if (x.questIncrease < y.questIncrease)
            return -1;
        if (x.questIncrease > y.questIncrease)
            return 1;

        return 0;
    }
}
