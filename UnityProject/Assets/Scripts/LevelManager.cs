using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using USceneManager = UnityEngine.SceneManagement.SceneManager;
using DG.Tweening;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public Transform spawnPoint;
    public float spawnRate = 1;

    public static LevelManager instance;

    public static bool init = false;

    public bool populateThis = true;

    [Header("References")]
    public GameObject playerPrefab;
    public Transform background;

    [Header("Debug")]
    public SpawnRegion[] backgroundRegions;

    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (!populateThis)
            return;

        DOTween.Init(); // empty param = default settings

        PopulateScene();

        
    }

    private void Update()
    {
        
    }

    public void PopulateScene()
    {
        Debug.Log("Populating!");
        backgroundRegions = background.GetComponentsInChildren<SpawnRegion>();

        GlyphBiome biome = GlyphManager.biome;
        int biomeIndex = 0;

        foreach (SpawnRegion assetArea in backgroundRegions)
        {
            Debug.Log("Before region!");
            if (biome.foreGround.Count == 0)
                break;
            Debug.Log("During region!");
            biomeIndex++;
            float factor = 1.1f;
            if (assetArea.beforePlayarea)
                factor = -1.1f;

            int toSpawn = Mathf.FloorToInt(spawnRate * assetArea.Range.x * biome.spawnrate);

            // Use scene information to populate stuff
            for (int i = 0; i < toSpawn; i++)
            {
                Debug.Log("Spawning!");
                float x = Random.value * assetArea.Range.x + assetArea.PositionMin.x;
                float y = assetArea.PositionMin.y;
                float z = factor * biomeIndex;

                float r = 0;
                if (assetArea.Range.y > 0)
                {
                    r = Random.value;
                    float h = r * assetArea.Range.y;
                    y += h;
                    z += r;
                }

                int index = GetWeightedIndex(GlyphManager.biome.foreGround.Cast<IWeighted>().ToList());
                GameObject newGO = Instantiate(GlyphManager.biome.foreGround[index].prefab);
                newGO.transform.position = new Vector3(x, y, z);
                newGO.transform.localScale = newGO.transform.localScale * Mathf.Lerp(assetArea.buttomScale, assetArea.topScale, r);
            }
            Debug.Log("After region!");
        }

        // Spawn player
        GameObject player = Instantiate(playerPrefab);
        GameManager.instance.player = player;
        player.transform.position = spawnPoint.position;
    }

    public void ChangeScene()
    {
        init = true;
        USceneManager.LoadScene(GlyphManager.landscape.sceneName);
    }

    public int GetWeightedIndex(List<IWeighted> weights)
    {
        if (weights.Count == 0)
            throw new System.Exception("The list of weights have no elements.");

        float total = 0;
        List<float> map = new List<float>();

        for (int i = 0; i < weights.Count; i++)
        {
            map.Add(total);
            total += weights[i].Weight;
        }

        float value = Random.value * total;

        for (int i = 0; i < map.Count; i++)
        {
            if (value < map[i])
            {
                return i - 1;
            }
        }

        return weights.Count - 1;
    }
}

public interface IWeighted
{
    public float Weight { get; }
}