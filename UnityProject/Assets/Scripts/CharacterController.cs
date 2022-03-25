using JStuff.TwoD.Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Body2d body;
    public BaseModifier running;
    public BaseModifier crouch;
    public BaseModifier bouncy;

    private Animator anim;
    private bool isWalking;

    private bool bouncing = false;

    private bool Flipped
    {
        get
        {
            return (transform.localScale.x < 0);
        }
        set
        {
            if (Flipped && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            } else if (!Flipped && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private void Start()
    {
        this.anim = GetComponentInChildren<Animator>();
        if (this.anim != null) Debug.Log("Character Animator initialized");
        isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.Space) && !bouncing)
            {
                body.ApplyFilter(bouncy);
                bouncing = true;
            }
            body.JumpInput(1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.HorizontalInput(-1);

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.HorizontalInput(1);
            SetWalking(true);
        }
        else if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            body.FallInput();
            SetWalking(true);
        } else {
            SetWalking(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && running != null)
        {
            body.ApplyFilter(running);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && running != null)
        {
            body.RemoveFilter(running);
        }


        if (Input.GetKeyDown(KeyCode.LeftControl) && crouch != null)
        {
            body.ApplyFilter(crouch);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && crouch != null)
        {
            body.RemoveFilter(crouch);
        }

        if (Input.GetKeyUp(KeyCode.Space) && bouncing)
        {
            body.RemoveFilter(bouncy);
            bouncing = false;
        }
    }

    private void SetWalking(bool isWalking)
    {
        anim.SetBool("isWalking", isWalking);
    }
}