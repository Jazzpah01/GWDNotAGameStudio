using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GaryCutscene : MonoBehaviour
{
    public bool started = false;
    public bool ended = false;

    public GameObject gary;
    public GameObject player;

    public Dialogue dialogue;
    private TextMeshPro text;
    public int currentLine;
    public bool playerLine;

    public Transform goalPos;

    public float talkDuration;
    public float walkDuration;
    public float lockedDuration;
    private float timer;

    private Color orange = new Color(0.5f, 0.5f, 0f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        dialogue.current = -1;
        player = GameManager.instance.player;
        player.GetComponent<CharacterController>().isInDialogue = true;
        text = gary.GetComponentInChildren<TextMeshPro>();
        //gary.GetComponent<NPCController>().dialogueActive = true;
        //gary.GetComponent<NPCController>().playerInRange = true;
        //gary.GetComponent<NPCController>().dialogue = d;

        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!ended && !started && timer > 2f)
        {
            timer = 0f;
            started = true;
            gary.GetComponent<NPCController>().MoveGary(goalPos.position, walkDuration);
            player.GetComponent<CharacterController>().isInCutscene = true;
            player.GetComponent<CharacterController>().WalkTo(goalPos.position + (new Vector3(-1f,0f,0f)), walkDuration);
        }

        if (!ended && started && timer > 2f)
        {
            timer = 0f;
            dialogue.current++;
            SetLine();

        }

        if (ended && timer > 2f) text.text = "";

    }

    public void EndCutscene()
    {
        player.GetComponent<CharacterController>().isInDialogue = false;
        player.GetComponent<CharacterController>().isInCutscene = false;
        ended = true;
    }

    private void SetLine()
    {
        if (dialogue.lines.Count < dialogue.current + 1)
        {
            EndCutscene();
            return;
        }

        Debug.Log("Setting line: " + dialogue.current);

        if (dialogue.lines[dialogue.current].isPlayer)
        {
            text.color = Color.red;

            text.gameObject.transform.position = (new Vector3(2.2f, 2f)) + player.transform.position;
        }
        else
        {
            //text.color = Color.yellow;
            text.color = orange;
            text.gameObject.transform.position = (new Vector3(2.2f, 2.5f)) + gary.transform.position;
        }

        text.text = dialogue.lines[dialogue.current].line;
        currentLine = dialogue.current;
    }
}
