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

    public SceneContext sceneContext;

    public bool finishedLoading = false;
    public bool loadingQuestEvents = true;

    [Header("References")]
    public GlyphLandscape landscapeGlyph;
    public Transform spawnRegions;
    public Transform spawnPoint;
    public EnvCamController environmentController;
    public PlaceManager placeManager;

    [Header("External References")]
    public GameObject backgroundPrefab;
    public GameObject sunPrefab;
    public GameObject playerPrefab;

    [Header("Debug")]
    public SpawnRegion[] backgroundRegions;

    System.Random rng;

    
    private void Awake()
    {
        instance = this;
        sceneContext = new SceneContext();

        
    }

    private void Start()
    {
        if (!InitialLevel.gameInitialized)
        {
            // TODO: set location glyph to match the scene.
            GlyphManager.landscape = landscapeGlyph;
            SceneManager.LoadScene(0);
            return;
        }

        if (!populateThis)
            return;

        StartCoroutine(Fader.FadeOut(1f));

        finishedLoading = false;

        DOTween.Init(); // empty param = default settings
        //SetupGlyphs();
        LoadPlace();
        SpawnRegionCollisions();
        PopulateScene();
        SetPlaceNPCs();
        ExecuteQuestEvents();

        InitialLevel.firstSceneLoaded = true;

        finishedLoading = true;
        
    }

    private void Update()
    {
        
    }

    public void SetupGlyphs()
    {
        if (!InitialLevel.firstSceneLoaded)
            return;

        GlyphManager.oldTimeIndex = GlyphManager.timeIndex;

        GlyphManager.timeIndex += (GlyphManager.GetIndex(GlyphManager.landscape) +
            GlyphManager.GetIndex(GlyphManager.biome));
        GlyphManager.timeIndex %= GlyphManager.collection.times.Count;
        GlyphManager.time = GlyphManager.collection.times[GlyphManager.timeIndex];
    }

    public void PopulateScene()
    {
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
        rng = new System.Random(GlyphManager.time.seed);
        foreach (SpawnRegion assetArea in backgroundRegions)
        {
            // Spawn regions
            biomeIndex++;
            float factor = 1f;

            float toSpawn = (spawnRate * assetArea.Range.x * biome.spawnrate * assetArea.spawnrate);

            // Use scene information to populate stuff
            float i = 0;
            while (i < toSpawn)
            {
                float x = (float)rng.NextDouble() % 1f * assetArea.Range.x + assetArea.PositionMin.x;
                float y = assetArea.PositionMin.y;
                float z = 0;

                float r = 0;
                if (assetArea.Range.y > 0)
                {
                    r = (float)rng.NextDouble() % 1f;
                    float h = r * assetArea.Range.y;
                    y += h;
                    z += r;
                }

                int index = GetWeightedIndex(GlyphManager.biome.foreGround.Cast<IWeighted>().ToList());
                GameObject newGO = Instantiate(GlyphManager.biome.foreGround[index].prefab);
                newGO.transform.position = new Vector3(x, y, z);
                newGO.transform.localScale = newGO.transform.localScale * Mathf.Lerp(assetArea.buttomScale, assetArea.topScale, r);
                newGO.transform.parent = transform;

                SpriteRenderer ren = newGO.GetComponent<SpriteRenderer>();

                if (assetArea.sortingLayer == SpawnRegion.Layer.BehindPlayer)
                {
                    ren.sortingLayerName = "Behind";
                } else if (assetArea.sortingLayer == SpawnRegion.Layer.InFrontOfPlayer)
                {
                    ren.sortingLayerName = "Front";
                } else
                {
                    throw new System.Exception("Layer is wrong");
                }

                ren.sortingOrder = assetArea.sortingOrder;

                i += GlyphManager.biome.foreGround[index].weight;
            }
        }
    }

    public void LoadPlace()
    {
        // Load place
        placeManager.biome = GlyphManager.biome;
        placeManager.time = GlyphManager.time;
        placeManager.LoadPlace();
    }

    public void SetPlaceNPCs()
    {
        NPCController[] npcs = placeManager.GetComponentsInChildren<NPCController>();
        foreach (NPCController npc in npcs)
        {
            sceneContext.AddNPC(npc);
        }
    }

    public void ExecuteQuestEvents()
    {
        List<QuestEvent> toExecute = new List<QuestEvent>();
        loadingQuestEvents = true;

        foreach (Quest quest in QuestManager.currentQuests)
        {
            for (int i = 0; i < quest.questEvents.Count; i++)
            {
                QuestEvent questEvent = quest.questEvents[i];

                if (questEvent.questIndex > quest.QuestIndex)
                    continue;

                QuestManager.SubscribeEvent(questEvent);
            }
        }

        QuestManager.ExecuteEvents();

        loadingQuestEvents = false;
    }

    public static void ChangeScene()
    {
        LevelManager.instance.SetupGlyphs();

        init = true;

        if (!InitialLevel.firstSceneLoaded)
        {
            print("Setting scene.");
            SetScene(GlyphManager.landscape.sceneName);
        } else
        {
            print("Setting transition scene.");
            LevelManager.instance.StartCoroutine( Fader.FadeIn(1f,
                delegate { SetScene("TransitionScene"); }
            ));
        }
    }

    public static void SetScene(string sceneName)
    {
        USceneManager.LoadScene(sceneName);
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

        float value = (float)rng.NextDouble() % 1f * total;

        for (int i = 0; i < map.Count; i++)
        {
            if (value < map[i])
            {
                return i - 1;
            }
        }

        return weights.Count - 1;
    }

    public void SpawnRegionCollisions()
    {
        backgroundRegions = spawnRegions.GetComponentsInChildren<SpawnRegion>();
        List<SpawnRegion> sp = backgroundRegions.ToList();
        Debug.Log(backgroundRegions.Length);

        foreach (NoSpawnRegion nosr in placeManager.currentPlace.GetComponentsInChildren<NoSpawnRegion>())
        {
            foreach (SpawnRegion sr in backgroundRegions.ToArray())
            {
                if (Region.Collision(nosr, sr))
                {
                    Debug.Log("Collisions!");
                    Destroy(sr.gameObject);
                    if (sp.Contains(sr))
                        sp.Remove(sr);
                } else
                {
                    
                }
            }
        }

        backgroundRegions = sp.ToArray();
        Debug.Log(backgroundRegions.Length);
    }
}



public interface IWeighted
{
    public float Weight { get; }
}