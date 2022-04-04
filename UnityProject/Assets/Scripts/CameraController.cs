using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private GameObject player;

    public float x_offset;
    public float y_offset;


    void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        //player = GameManager.instance.player;
        //gameObject.transform.position = new Vector3(player.transform.position.x + x_offset, player.transform.position.y + y_offset, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 npos = transform.position;

        npos.x = player.transform.position.x;

        transform.position = npos;
        */
        if (GameManager.instance.player != null)
        {
            //gameObject.transform.position = new Vector3(GameManager.instance.player.transform.position.x + x_offset, GameManager.instance.player.transform.position.y + y_offset, -10f);
            gameObject.transform.position = new Vector3(GameManager.instance.player.transform.position.x + x_offset, y_offset, -10f);
            // TO-DO: Add Camera lerping on player jumping etc.
        }
    }
}
