using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform player;

    public float x_offset;
    public float y_offset;

    public bool jumping;

    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        //player = GetComponentInParent<Transform>();
        if (GameManager.instance != null)
        {
            player = GameManager.instance.player.transform;
            //transform.position = player.position + new Vector3(x_offset, y_offset, -10f);
        }
    }

    void Update()
    {
        //gameObject.transform.position = new Vector3(GameManager.instance.player.transform.position.x + x_offset, GameManager.instance.player.transform.position.y + y_offset, -10f);
        //transform.position = new Vector3(player.position.x + x_offset, y_offset, -10f);
        // TO-DO: Add Camera lerping on player jumping etc.
        //transform.position = player.position
        //transform.position = transform.position + 

        /*
        if (transform.position.y != player.position.y + y_offset)
        {
            jumping = true;
            transform.DOLocalMoveY(player.position.y + y_offset, 0.5f).SetEase(Ease.InSine).OnComplete(delegate { jumping = false; });
        }
        */
        transform.position = new Vector3(transform.position.x, y_offset, transform.position.z);
    }

}
