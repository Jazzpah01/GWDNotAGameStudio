using JStuff.TwoD.Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterController : MonoBehaviour
{
    [Header("FMOD")]
    public FMODUnity.EventReference bumpEvent;
    public FMODUnity.EventReference aquiredGlyph;
    public FMODUnity.StudioEventEmitter walkEmitter;
    public FMODUnity.StudioEventEmitter music1Emitter;
    public FMODUnity.StudioEventEmitter music2Emitter;
    public FMODUnity.EventReference defaultVoice;

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
    private bool grounded = false;


    //quest and dialogue stuff:
    public bool isInDialogue;
    public int currentQuest;

    public bool isInCutscene = false;

    public static CharacterController instance;

    public static bool playerBusy = false;


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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        this.anim = GetComponentInChildren<Animator>();
        //if (this.anim != null) Debug.Log("Character Animator initialized");
        isWalking = false;
        FlipSprite(false);

        if (Random.value < 0.5f)
        {
            music1Emitter.Play();
        } else
        {
            music2Emitter.Play();
        }
    }

    private void OnDisable()
    {
        music1Emitter.Stop();
        music2Emitter.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!grounded && body.grounded)
            FMODUnity.RuntimeManager.PlayOneShot(bumpEvent);

        grounded = body.grounded;

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
                //walkEvent.start();
                if (!walkEmitter.IsPlaying())
                    walkEmitter.Play();

                SetWalking(true);
                FlipSprite(true);
                //print("Walking");
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                body.HorizontalInput(1);
                //walkEvent.start();
                if (!walkEmitter.IsPlaying())
                    walkEmitter.Play();

                SetWalking(true);
                FlipSprite(false);
                //print("Walking");
            }
            else
            {
                SetWalking(false);
                //print("Stopping");
                walkEmitter.Stop();
                //walkEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
        } else if (!isInCutscene)
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
        if (flip)
        {
            rig.localScale = new Vector3(-1f, 1f, 1f);
        } else
        {
            rig.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void WalkTo(Vector2 goalPos, float duration)
    {
        FlipSprite(goalPos.x < transform.position.x);
        SetWalking(true);

        transform.DOMove(goalPos, duration).SetEase(Ease.Linear).OnComplete(delegate { SetWalking(false); });

    }

    private void StopWalking()
    {
        SetWalking(false);
    }
}