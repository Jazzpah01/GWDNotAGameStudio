using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    public string ID;

    [Header("References")]
    public GameObject interactText;


    public bool playerInside = false;
    public bool isInteractable;

    IInteractant interactant;
    Action callback;

    private void Awake()
    {
        interactant = GetComponent<IInteractant>();
        ID = gameObject.name.Replace("(Clone)","");
        isInteractable = false;
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
            interactText.SetActive(true);

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
        if (!(playerInside && Input.GetKeyDown(KeyCode.E) && isInteractable))
            return;

        playerInside = false;
        interactText.SetActive(false);

        // Player hit collider AND pressed E
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

    public void SetInteractable(Action callback = null)
    {
        this.callback = callback;
        this.isInteractable = true;
    }

    public void SetUninteractable()
    {
        this.callback = null;
        this.isInteractable = false;
        this.interactText.SetActive(false);
    }
}