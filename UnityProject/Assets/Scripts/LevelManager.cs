using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using USceneManager = UnityEngine.SceneManagement.SceneManager;
using DG.Tweening;


public class LevelManager : MonoBehaviour
{
    public Transform spawnPoint;
    public Stretchable assetArea;
    public float spawnRate;

    [Header("References")]
    public GameObject playerPrefab;

    private void Start()
    {
        DOTween.Init();

        int toSpawn = Mathf.FloorToInt(spawnRate * assetArea.Range.x);

        // Use scene information to populate stuff
        foreach (TerrainObject to in GlyphManager.biome.foreGround)
        {
            for (int i = 0; i < toSpawn; i++)
            {
                float x = Random.value * assetArea.Range.x + assetArea.PositionMin.x;
                float y = assetArea.PositionMin.y;
                //GameObject newGO = Instantiate()
            }
        }

        // Spawn player
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = spawnPoint.position;
    }

    public void ChangeScene()
    {
        USceneManager.LoadScene(GlyphManager.landscape.sceneName);
    }

    //public int GetWeightedIndex(List<IWeighted> weights)
    //{
    //    
    //}
}

public interface IWeighted
{
    public float Weight { get; }
}