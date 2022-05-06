using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    [Header("Options")]
    public bool startInteractable = false;
    public bool interactOnEnter = false;

    [Header("References")]
    public GameObject interactText;

    [Header("Debug")]
    public string ID;
    public bool playerInside = false;
    public bool isInteractable;

    IInteractant interactant;
    Action callback;

    private void Awake()
    {
        interactant = GetComponent<IInteractant>();
        ID = gameObject.name.Replace("(Clone)","");
        interactText.SetActive(false);
        playerInside = false;
        LevelManager.instance.sceneContext.AddInteractable(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        playerInside = true;
        if (isInteractable)
        {
            if (interactOnEnter)
            {
                Interact();
            } else
            {
                interactText.SetActive(true);
            }
        }
            

        if (interactant != null)
            interactant.InRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        playerInside = false;
        interactText.SetActive(false);

        if (interactant != null)
            interactant.InRange = true;
    }

    private void Update()
    {
        if (!(playerInside && Input.GetKeyDown(KeyCode.E) && isInteractable && !CharacterController.playerBusy))
            return;

        // Player hit collider AND pressed E
        Interact();
    }

    public void SetInteractable(Action callback = null)
    {
        this.callback = callback;
        this.isInteractable = true;
        if (playerInside)
            interactText.SetActive(true);
    }

    public void SetUninteractable()
    {
        this.callback = null;
        this.isInteractable = false;
        this.interactText.SetActive(false);
    }

    public void Interact()
    {
        interactText.SetActive(false);
        if (interactant == null)
        {
            if (callback != null)
                callback();
        }
        else
        {
            if (callback != null)
                callback();
            interactant.Interact();
        }
    }
}