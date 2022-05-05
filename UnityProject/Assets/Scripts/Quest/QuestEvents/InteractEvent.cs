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
            if (item.ID == interactableObject.name)
            {
                Debug.Log("Succes on execute!!!! " + setInteractable);
                if (setInteractable)
                {
                    item.SetInteractable(CallBack);
                } else
                {
                    item.SetUninteractable();
                }
            }
        }

        Debug.Log($"NO INTERACT: {interactableObject.name}");
    }

    public override bool ShouldExecute(SceneContext context)
    {
        Interactable intera = interactableObject.GetComponent<Interactable>();

        foreach (Interactable item in context.interactables)
        {
            Debug.Log($"COMPARING {item.ID}...{interactableObject.name}");
            if (item.ID == interactableObject.name)
            {
                Debug.Log("Succes on should!!!!");
                return true;
            }
        }
        Debug.Log($"NO INTERACT ON SHOULD: {interactableObject.name}");
        return false;
    }
}