using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    GameManager game;

    public bool hasMet;
    public bool hasDialogue;

    private void Awake()
    {
        game = GameManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // activate dialogue prompt
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
