using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TimelineController : MonoBehaviour
{

    //public TimelinePlayable director;
    public TimelinePlayable director;
    
    public GameObject cutscenePlayer;
    public GameObject cutsceneNPC;

    public GameObject playerHolder;

    public bool init;
    public bool ended;

    // Start is called before the first frame update
    void Start()
    {
        //director = gameObject.GetComponent<TimelinePlayable>();
    }

    // Update is called once per frame
    void Update()
    {
        //GameManager.instance.player.SetActive(false);

        if (!init)
        {
            playerHolder = GameManager.instance.player;
            //GameManager.instance.player = cutscenePlayer;
            EnvCamController.instance.player = cutscenePlayer;
            init = true;
        }
    }
}
