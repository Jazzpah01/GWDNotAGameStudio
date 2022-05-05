using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI instance;
    private GameManager game;

    public GameObject container;
    public TextMeshProUGUI tmp;
    public Image playerPortrait;
    public Image npcPortrait;

    public Dialogue dialogue;

    public GameObject player;
    public NPCController npc;
    public Action callback;

    public bool dialogueAvailable;
    public bool dialogueActive;
    public bool animActive;
    public bool isEntry;
    public int currentLine;
    public float inputDelay;
    private float inputTimer;


    //private TextMeshPro text;
    private string writer;
    private Color defaultTextColor;

    private float timer;
    public float delayInterval;

    float delayBeforeStart = 0f;
    float timeBtwChars = 0.1f;
    string leadingChar = "";
    bool leadingCharBeforeDelay = false;

    private float y_out;
    private float y_in;
    public float animSpeed;


    private void Awake()
    {
        if (GameManager.instance != null) game = GameManager.instance;
        instance = this;
        y_out = transform.position.y;
        y_in = y_out + 260f;
    }

    
    void Start()
    {
        player = GameManager.instance.player;
        if (player == null) Debug.Log("DialogueUI: Player reference is NULL");
        dialogueAvailable = false;
        dialogueActive = false;
        animActive = false;
        isEntry = false;


        //this.gameObject.transform.position = new Vector2(0, y_out);
    }

    
    void Update()
    {
        inputTimer += Time.deltaTime;

        if (player != null) {
            player.GetComponent<CharacterController>().isInDialogue = dialogueActive;
        } else
        {
            player = GameManager.instance.player;
        }

        if (!animActive && dialogueActive && !isEntry) // starting dialogue
        {
            EntryUI();
            dialogue.current = -1;
            DNext();
            Debug.Log("DialogueUI: Entering Dialogue...");
        } else if (!animActive && !dialogueActive && isEntry) // ending dialogue
        {
            ExitUI();
            Debug.Log("DialogueUI: Finishing Dialogue...");
        }

        if (dialogueActive && !animActive && isEntry && inputTimer > inputDelay) { 
            if (Input.GetKey(KeyCode.E)) DNext();
            if (Input.GetKey(KeyCode.Q)) DPrevious();
        }
    }

    public void StartDialogue()
    {
        if (!dialogueActive && !animActive && !isEntry)
        {
            dialogueActive = true;
            Debug.Log("DialogueUI: Dialogue successfully forced active!");
        } else if (dialogueActive)
        {
            Debug.Log("DialogueUI: Dialogue already active..");
        } else if (animActive)
        {
            Debug.Log("DialogueUI: Animation ongoing..");
        }
        else if (isEntry)
        {
            Debug.Log("DialogueUI: Dialogue window already open..");
        }
    }

    private void EntryUI()
    {
        animActive = true;
        this.gameObject.transform.DOMoveY(y_in, animSpeed).OnComplete(delegate
        {
            animActive = false;
            isEntry = true;
        });
    }

    private void ExitUI()
    {
        animActive = true;
        this.gameObject.transform.DOMoveY(y_out, animSpeed).OnComplete(delegate
        {
            animActive = false;
            isEntry = false;
        });
    }

    public void SetDialogue(NPCController npc, Dialogue d, Action callback = null)
    {

        if (d == null)
        {
            this.dialogue = null;
            this.callback = null;
        } else
        {
            this.dialogue = d;
            this.callback = callback;
            dialogue.current = -1;
        }

        if (npc != null && npc.npcPortrait != null)
        {
            this.npc = npc;
            npcPortrait = npc.npcPortrait;
        } else
        {
            Debug.Log("DialogueUI.SetDialogue(): Dialogue NPC has no Portrait!");
        }
    }

    private void DNext()
    {
        inputTimer = 0f;
        if (dialogue.lines.Count <= dialogue.current + 1)
        {
            dialogueActive = false;
            dialogue.SetComplete(true);
            if (callback != null) callback(); //callback for quest progression
        } else
        {
            dialogue.current++;
            SetLine();
        }
    }

    private void DPrevious()
    {
        inputTimer = 0f;
        if (dialogue.current - 1 < 0)
        {
            dialogue.current = -1;
            dialogueActive = false; // TODO: fix with animation
        } else
        {
            dialogue.current--;
            SetLine();
        }
    }


    private void SetLine()
    {
        //if (dialogue.current > dialogue.lines.Count) return;
        //tmp.text

        if (dialogue.lines[dialogue.current].isPlayer)
        {
            tmp.alignment = TextAlignmentOptions.TopLeft;
            playerPortrait.enabled = true;
            npcPortrait.enabled = false;
        } else
        {
            tmp.alignment = TextAlignmentOptions.TopRight;
            playerPortrait.enabled = false;
            npcPortrait.enabled = true;
        }

        tmp.text = dialogue.lines[dialogue.current].line;

        if (!dialogue.lines[dialogue.current].voice.IsNull)
        {
            FMODUnity.RuntimeManager.PlayOneShot(dialogue.lines[dialogue.current].voice);
        } else if (dialogue.lines[dialogue.current].isPlayer)
        {
            CharacterController player = LevelManager.instance.playerPrefab.GetComponent<CharacterController>();
            FMODUnity.RuntimeManager.PlayOneShot(player.defaultVoice);
        } else
        {
            FMODUnity.RuntimeManager.PlayOneShot(npc.defaultVoice);
        }

        currentLine = dialogue.current;

        Debug.Log("DialogueUI: line " + currentLine + " set!");
    }

    
}
