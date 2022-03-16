using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvCamController : MonoBehaviour
{

    private GameObject player;

    private GameObject BG_child;
    public GameObject Sun;

    public Vector3 originPos = new Vector3();
    public Vector3 currentPos;

    public float BG_multiplier;
    public float SUN_mult;
    private Vector3 sunOrigin;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
        BG_child = GetComponentInChildren<SpriteRenderer>().gameObject;
        BG_child.transform.position = originPos;
        Debug.Log("Env Init at: " + BG_child.transform.position + " == " + originPos);

        if (Sun != null) sunOrigin = Sun.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //BG_child.transform.position.x = player.transform.position.x; // 

        BG_child.transform.position = new Vector3(player.transform.position.x * BG_multiplier, originPos.y, originPos.z);

        if (Sun != null)
        {
            Sun.transform.position = new Vector3(sunOrigin.x + (player.transform.position.x * SUN_mult), sunOrigin.y, sunOrigin.z);
        }
    }

    private float GetPlayerOriginOffset()
    {
        return 0f;
    }
}
