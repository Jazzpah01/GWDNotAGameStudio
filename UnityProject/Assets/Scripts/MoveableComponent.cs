using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableComponent : MonoBehaviour, IMoveable
{
    public Animator animator;
    public GameObject rig;

    private Interactable interactable;

    private bool wasInteractable = false;
    private bool isMoving = false;
    private Vector2 targetMovingPosition = Vector2.zero;
    private Action movingCallback = null;
    private Action interactableCallback = null;

    public float MoveableSpeed { get; set; }

    public string ID => gameObject.name.Replace("(Clone)", "");


    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector2 direction = targetMovingPosition - (Vector2)transform.position;

            if (direction == Vector2.zero)
            {
                ReachedGoal();
            }

            transform.position += (Vector3)direction.normalized * MoveableSpeed * Time.deltaTime;

            if (Vector2.Dot(targetMovingPosition - (Vector2)transform.position, direction) < 0)
            {
                Debug.Log("YAS GIRL WALK");
                ReachedGoal();
            }

            if (direction.x > 0)
            {
                Vector3 scale = rig.transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                rig.transform.localScale = scale;
            } else
            {
                Vector3 scale = rig.transform.localScale;
                scale.x = -Mathf.Abs(scale.x);
                rig.transform.localScale = scale;
            }
        }
    }

    public void MoveTo(Vector2 position, Action callback = null)
    {
        wasInteractable = interactable.isInteractable;
        interactableCallback = interactable.callback;
        interactable.callback = null;

        interactable.SetUninteractable();

        movingCallback = callback;
        isMoving = true;
        targetMovingPosition = position;

        animator.SetBool("isWalking", true);
    }

    public void ReachedGoal()
    {
        if (wasInteractable && !interactable.isInteractable)
        {
            interactable.SetInteractable(interactableCallback);
        }

        isMoving = false;
        if (movingCallback != null)
            movingCallback();

        animator.SetBool("isWalking", false);
    }

    public void InstantMove(Vector2 position)
    {
        transform.position = position;
    }
}