using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvCamController : MonoBehaviour
{
    public static EnvCamController instance;

    private GameObject player;

    public GameObject Background;
    public GameObject Sun;

    public Vector3 originPos = new Vector3();
    public Vector3 currentPos;

    public float BG_multiplier;
    public float SUN_mult;
    private Vector3 sunOrigin;

    // cloud stuff
    public GameObject cloud_prefab;
    public Vector3 cloud_spawn; // TODO: decide cloud spawn position
    private float cloud_timer;
    public float cloud_interval;
    public int cloud_capacity;
    public int clouds_active;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;

        cloud_timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //BG_child.transform.position.x = player.transform.position.x; // 
        if (player == null && GameManager.instance.player != null) player = GameManager.instance.player;

        Background.transform.position = new Vector3(player.transform.position.x * BG_multiplier, originPos.y, originPos.z);

        if (Sun != null)
        {
            Sun.transform.position = new Vector3(sunOrigin.x + (player.transform.position.x * SUN_mult), sunOrigin.y, sunOrigin.z);
        }

        // cloud spawning
        cloud_timer += Time.deltaTime;
        if (GameManager.instance.player != null && cloud_prefab != null && cloud_timer > cloud_interval && clouds_active <= cloud_capacity)
        {
            Vector3 playerPos = GameManager.instance.player.transform.position;
            Vector3 spawnPos = new Vector3(playerPos.x - 10f, playerPos.y + 5f, 0f);    // TODO: replace magic numbers with camera dimensions
            GameObject cloud = Instantiate(cloud_prefab);
            cloud.transform.position = spawnPos;
            clouds_active++;
            cloud_timer = 0f;
            Debug.Log("Cloud Spawned! (pos: " + spawnPos + " ) - Number of active clouds = " + clouds_active);
        }
        else if (cloud_timer > cloud_interval)
        {
            cloud_timer = 0f;
        }
    }

    public void SpawnSun(GameObject prefab)
    {
        Sun = Instantiate<GameObject>(prefab);

        Sun.GetComponent<SpriteRenderer>().sprite = GlyphManager.time.sunSprite;

        sunOrigin = (Vector3)GlyphManager.time.sunPosition + transform.position;
    }

    public void SpawnBackground(GameObject prefab)
    {
        Background = Instantiate<GameObject>(prefab);

        //TODO set sprite of background

        Background.transform.position = originPos;
    }

    private float GetPlayerOriginOffset()
    {
        return 0f;
    }
}
