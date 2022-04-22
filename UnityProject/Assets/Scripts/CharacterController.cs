using JStuff.TwoD.Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("FMOD")]
    public FMODUnity.EventReference flipSound;
    public FMODUnity.EventReference walkSound;

    [Header("Other")]
    public Body2d body;
    public BaseModifier running;
    public BaseModifier crouch;
    public BaseModifier bouncy;

    //public SpriteRenderer[] sprites;

    private Animator anim;
    private bool isWalking;
    //public bool isFlipped;
    public Transform rig;

    private bool bouncing = false;


    //quest and dialogue stuff:
    public bool isInDialogue;
    public int currentQuest;


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
        //sprites = anim.gameObject.GetComponentsInChildren<SpriteRenderer>();
        FlipSprite(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInDialogue)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.Space) && !bouncing)
                {
                    //body.ApplyFilter(bouncy);
                    //bouncing = true;
                }
                body.JumpInput(1);
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                body.FallInput();
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                body.HorizontalInput(-1);
                SetWalking(true);
                FlipSprite(true);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                body.HorizontalInput(1);
                SetWalking(true);
                FlipSprite(false);
            }
            else
            {
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
        } else
        {
            SetWalking(false);
        }
    }

    private void SetWalking(bool isWalking)
    {
        anim.SetBool("isWalking", isWalking);
    }

    private void FlipSprite(bool flip)
    {
        //SpriteRenderer[] sprites = anim.gameObject.GetComponentsInChildren<SpriteRenderer>();
        //anim.gameObject.GetComponentsInChildren<SpriteRenderer>().flipX = isFlipped;
        /*
        foreach (SpriteRenderer sprite in sprites)
        {
            //sprite.flipX = flip;
            //Vector3 spritePos = sprite.transform.position;

            if (flip) {
                sprite.transform.localScale = new Vector3(-1f, 1f, 1f);
            } else {
                sprite.transform.localScale = new Vector3(1f, 1f, 1f);
            } 
        }
        */
        if (flip)
        {
            rig.localScale = new Vector3(-1f, 1f, 1f);
            FMODUnity.RuntimeManager.CreateInstance(flipSound.Guid);
        } else
        {
            rig.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}