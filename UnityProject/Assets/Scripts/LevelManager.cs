using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using USceneManager = UnityEngine.SceneManagement.SceneManager;

public class LevelManager : MonoBehaviour
{
    public Transform spawnPoint;
    public Stretchable assetArea;

    [Header("References")]
    public GameObject playerPrefab;

    private void Start()
    {
        // Use scene information to populate stuff

        // Spawn player
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = spawnPoint.position;
    }

    public void ChangeScene()
    {
        USceneManager.LoadScene(GlyphManager.landscape.sceneName);
    }
}