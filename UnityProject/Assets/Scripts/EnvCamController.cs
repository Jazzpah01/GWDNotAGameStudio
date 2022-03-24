using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvCamController : MonoBehaviour
{
    public static EnvCamController instance;

    private GameObject player;

    private GameObject BG_child;
    public GameObject Sun;

    public Vector3 originPos = new Vector3();
    public Vector3 currentPos;

    public float BG_multiplier;
    public float SUN_mult;
    private Vector3 sunOrigin;

    // cloud stuff
    public GameObject cloud_prefab;
    public Vector2 cloud_spawn; // TODO: decide cloud spawn position
    private float cloud_timer;
    public float cloud_interval;
    public int cloud_capacity;
    public int clouds_active;
    public bool cloud_moves_right;  
    public float cloud_speed;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
        BG_child = GetComponentInChildren<SpriteRenderer>().gameObject;
        BG_child.transform.position = originPos;
        Debug.Log("Env Init at: " + BG_child.transform.position + " == " + originPos);

        if (Sun != null) sunOrigin = Sun.transform.position;

        cloud_timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //BG_child.transform.position.x = player.transform.position.x; // 
        if (player == null && GameManager.instance.player != null) player = GameManager.instance.player;

        BG_child.transform.position = new Vector3(player.transform.position.x * BG_multiplier, originPos.y, originPos.z);

        if (Sun != null)
        {
            Sun.transform.position = new Vector3(sunOrigin.x + (player.transform.position.x * SUN_mult), sunOrigin.y, sunOrigin.z);
        }

        // cloud spawning
        cloud_timer += Time.deltaTime;
        if (GameManager.instance.player != null && cloud_prefab != null && cloud_timer > cloud_interval && clouds_active <= cloud_capacity)
        {
            Vector3 playerPos = GameManager.instance.player.transform.position;
            Vector3 spawnPos = new Vector3(playerPos.x + cloud_spawn.x, cloud_spawn.y, 0f);
            GameObject cloud = Instantiate(cloud_prefab);
            cloud.transform.position = spawnPos;
            cloud.GetComponent<CloudController>().SetSpeed(cloud_speed);
            cloud.GetComponent<CloudController>().SetXOffset(cloud_spawn.x);
            cloud.GetComponent<CloudController>().SetMovesRight(cloud_moves_right);
            clouds_active++;
            cloud_timer = 0f;
            Debug.Log("Cloud Spawned! (pos: " + spawnPos + " ) - moves right: " + cloud_moves_right + " - Number of active clouds = " + clouds_active);
        }
        else if (cloud_timer > cloud_interval)
        {
            cloud_timer = 0f;
        }
    }



    private float GetPlayerOriginOffset()
    {
        return 0f;
    }
}
