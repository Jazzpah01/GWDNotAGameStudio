using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NPCController : MonoBehaviour
{
    GameManager game;
    private GameObject player;

    public string characterName;

    public GameObject rig;

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

    private void Awake()
    {
        game = GameManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {

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

        if (!playerInRange || dialogue == null)
        {
            ClearText();
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


                //playerInRange = false; // TODO: fix this ugly ass hack
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
        this.dialogue = dialogue;
        this.callback = callback;
        dialogue.current = -1;
    }


    private void DNext()
    {
        delayTimer = 0f;

        if (dialogue.lines.Count < dialogue.current + 1)
        {
            //dialogueActive = false;
            hasFinishedDialogue = true;
            dialogue.SetComplete(true); // TODO: Link this up with quest progression
            //EndDialoguePrompt();
        } else
        {
            dialogue.current++;
            SetLine();
        }
        Debug.Log("---next---current line: " + dialogue.current);
    }

    private void DPrevious()
    {
        delayTimer = 0f;

        if (dialogue.current - 1 < 0)
        {
            dialogue.current = -1;
            StartDialoguePrompt();
        } else {
            dialogue.current--;
            SetLine();
        }
        Debug.Log("---pre---current line: " + dialogue.current);
    }


    private void SetLine()
    {
        text.text = dialogue.lines[dialogue.current].line;
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
}
