using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class NPCController : MonoBehaviour
{
    GameManager game;
    private GameObject player;

    public string characterName;

    public GameObject rig;
    private Animator anim;
    private bool isWalking;

    public bool hasMet;
    public bool hasDialogue;
    public bool hasFinishedDialogue;
    public bool playerInRange = false;
    public bool dialogueActive = false;

    public Dialogue dialogue;
    public Action callback;
    public GameObject dialogueUI;
    public int currentLine;

    private TextMeshPro text;
    private float delayTimer;
    public float delayInterval = 0.2f;

    private Color orange = new Color(0.5f, 0.5f, 0f, 1f);


    private void Awake()
    {
        game = GameManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.anim = rig.GetComponent<Animator>();
        if (GameManager.instance != null) player = GameManager.instance.player;
        text = dialogueUI.GetComponent<TextMeshPro>();
        if (text != null) Debug.Log("text init success");
        ClearText();
        if (dialogue != null) dialogue.current = -1;
        delayTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        delayTimer += Time.deltaTime;

        player.GetComponent<CharacterController>().isInDialogue = dialogueActive;

        if (!playerInRange || dialogue == null)
        {
            //ClearText();
            dialogueActive = false;
            return;
        }

        if (dialogueActive && !playerInRange) dialogueActive = false;

        if (playerInRange)
        {
            if (player.transform.position.x < transform.position.x) // turn npc towards player
            {
                rig.transform.localScale = new Vector3(-1f, 1f, 1f);
            } else
            {
                rig.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }


        if (playerInRange && !dialogueActive)
        {
            StartDialoguePrompt();
        }

        if (Input.GetKey(KeyCode.E) && playerInRange)
        {
            dialogueActive = true;
            hasMet = true;
        }

        if (playerInRange && dialogueActive && delayTimer > delayInterval)
        {
            
            if (Input.GetKeyDown(KeyCode.E)) DNext();
            if (Input.GetKeyDown(KeyCode.Q)) DPrevious();
        }

        if (hasFinishedDialogue && dialogue.current == dialogue.lines.Count)
        {
            EndDialoguePrompt();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("ENDING DIALOGUE");
                ClearText();
                dialogueActive = false;
                playerInRange = false; // TODO: fix this ugly ass hack
            }
        }
    }

    /// <summary>
    /// Set Dialogue for NPC
    /// </summary>
    /// <param name="dialogue"></param>
    /// <param name="callback"></param>
    public void SetDialogue(Dialogue dialogue, Action callback = null)
    {
        if (dialogue == null)
        {
            this.dialogue = null;
            this.callback = null;
        } else
        {
            this.dialogue = dialogue;
            this.callback = callback;
            dialogue.current = -1;
        }
    }


    private void DNext()
    {
        delayTimer = 0f;

        if (dialogue.lines.Count < dialogue.current + 1)
        {
            //dialogueActive = false;
            hasFinishedDialogue = true;
            dialogue.SetComplete(true); // TODO: Link this up with quest progression
            if (callback != null)
                callback();
            //EndDialoguePrompt();
        } else
        {
            dialogue.current++;
            SetLine();
        }
        currentLine = dialogue.current;
        Debug.Log("---next---current line: " + dialogue.current);
    }

    private void DPrevious()
    {
        delayTimer = 0f;

        if (dialogue.current - 1 < 0)
        {
            dialogue.current = -1;
            StartDialoguePrompt();
            dialogueActive = false;
        } else {
            dialogue.current--;
            SetLine();
        }
        currentLine = dialogue.current;
        Debug.Log("---pre---current line: " + dialogue.current);
    }


    private void SetLine()
    {
        if (dialogue.current >= dialogue.lines.Count)
            return;


        if (dialogue.lines[dialogue.current].isPlayer)
        {
            text.color = Color.red;

            text.gameObject.transform.position = (new Vector3(2.2f, 2f)) + player.transform.position;
        } else
        {
            //text.color = Color.yellow;
            text.color = orange;
            text.gameObject.transform.position = (new Vector3(2.2f, 2.5f)) + gameObject.transform.position;
        }

        text.text = dialogue.lines[dialogue.current].line;
        currentLine = dialogue.current;

        Debug.Log("Set Line: " + dialogue.lines[dialogue.current].line);
        // TODO: Make text distinct based on who talks
    }

    private void DisplayPlayerLine(string s)
    {
        //
    }

    private void DisplayNPCLine(string s)
    {
        //
    }

    private void StartDialoguePrompt()
    {
        text.text = "( Press [E] to start conversation! )";
        dialogue.current = -1;
    }

    private void EndDialoguePrompt()
    {
        text.text = "( Press [E] to end conversation... )";
    }

    private void ClearText()
    {
        text.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // activate dialogue prompt
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }

    }

    public void MoveGary(Vector2 goalPos, float duration)
    {
        
        FlipSprite(goalPos.x < transform.position.x);
        SetWalking(true);
        
        transform.DOMove(goalPos, duration).SetEase(Ease.Linear).OnComplete(EndMove);
        
    }

    private void EndMove()
    {
        player.GetComponent<CharacterController>().isInDialogue = false;
        rig.SetActive(false);
        //Destroy(this.gameObject);
    }

    private void SetWalking(bool isWalking)
    {
        anim.SetBool("isWalking", isWalking);
    }

    private void FlipSprite(bool flip)
    {
        if (flip)
        {
            rig.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            rig.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
