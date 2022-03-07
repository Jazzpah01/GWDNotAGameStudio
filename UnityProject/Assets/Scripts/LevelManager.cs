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
    public Region assetArea;
    public float spawnRate = 1;

    public static LevelManager instance;

    [Header("References")]
    public GameObject playerPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DOTween.Init();

        Biome biome = GlyphManager.biome;

        int toSpawn = Mathf.FloorToInt(spawnRate * assetArea.Range.x * biome.spawnrate);

        // Use scene information to populate stuff
        for (int i = 0; i < toSpawn; i++)
        {
            float x = Random.value * assetArea.Range.x + assetArea.PositionMin.x;
            float y = assetArea.PositionMin.y;
            int index = GetWeightedIndex(GlyphManager.biome.foreGround.Cast<IWeighted>().ToList());
            GameObject newGO = Instantiate(GlyphManager.biome.foreGround[index].prefab);
            newGO.transform.position = new Vector3(x, y, 0.1f);
        }

        // Spawn player
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = spawnPoint.position;
    }

    public void ChangeScene()
    {
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