using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using USceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    public Vector3 spawnPoint;

    private void Start()
    {
        // Use scene information to populate stuff

        // Spawn player
    }

    public void ChangeScene()
    {
        USceneManager.LoadScene(GlyphManager.landscape.sceneName);
    }
}