using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractEvent : QuestEvent
{
    public override QuestEventType questEventType => QuestEventType.SetInteractable;

    public GameObject interactableObject;
    public bool setInteractable;

    public override void Execute(SceneContext context)
    {
        if (interactableObject == null)
            throw new System.Exception("interactableObject is null!");

        Interactable intera = interactableObject.GetComponent<Interactable>();

        foreach (Interactable item in context.interactables)
        {
            if (item == null)
                continue;

            if (item.ID == interactableObject.name)
            {
                if (setInteractable)
                {
                    item.SetInteractable(CallBack);
                } else
                {
                    item.SetUninteractable();
                }
            }
        }
    }

    public override bool ShouldExecute(SceneContext context)
    {
        Interactable intera = interactableObject.GetComponent<Interactable>();

        foreach (Interactable item in context.interactables)
        {
            if (item.ID == interactableObject.name)
            {
                return true;
            }
        }
        return false;
    }
}