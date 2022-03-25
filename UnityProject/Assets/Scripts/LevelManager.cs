using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using USceneManager = UnityEngine.SceneManagement.SceneManager;
using DG.Tweening;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public static bool init = false;

    public bool populateThis = true;
    public float spawnRate = 1;

    [Header("References")]
    public Transform spawnRegions;
    public Transform spawnPoint;
    public EnvCamController environmentController;

    [Header("External References")]
    public GameObject backgroundPrefab;
    public GameObject sunPrefab;
    public GameObject playerPrefab;

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

        if (!InitialLevel.gameInitialized)
        {
            // TODO: set location glyph to match the scene.
            SceneManager.LoadScene(0);
        }

        DOTween.Init(); // empty param = default settings
        SetupGlyphs();
        PopulateScene();

        
    }

    private void Update()
    {
        
    }

    public void SetupGlyphs()
    {
        GlyphManager.timeIndex = (GlyphManager.timeIndex + 1) % GlyphManager.times.Count;
        GlyphManager.time = GlyphManager.times[GlyphManager.timeIndex];
    }

    public void PopulateScene()
    {
        backgroundRegions = spawnRegions.GetComponentsInChildren<SpawnRegion>();

        // Spawn player
        GameObject player = Instantiate(playerPrefab);
        GameManager.instance.player = player;
        player.transform.position = spawnPoint.position;

        // Spawn background
        environmentController.SpawnBackground(backgroundPrefab);

        // Spawn sun
        environmentController.SpawnSun(sunPrefab);

        // Spawn regions
        GlyphBiome biome = GlyphManager.biome;
        int biomeIndex = 0;
        foreach (SpawnRegion assetArea in backgroundRegions)
        {
            // Spawn regions
            biomeIndex++;
            float factor = 1f;

            int toSpawn = Mathf.FloorToInt(spawnRate * assetArea.Range.x * biome.spawnrate * assetArea.spawnrate);

            // Use scene information to populate stuff
            for (int i = 0; i < toSpawn; i++)
            {
                float x = Random.value * assetArea.Range.x + assetArea.PositionMin.x;
                float y = assetArea.PositionMin.y;
                float z = 0;

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
                newGO.GetComponent<SpriteRenderer>().sortingLayerID = assetArea.sortingLayer;
            }
        }
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