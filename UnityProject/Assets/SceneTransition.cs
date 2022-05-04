using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JStuff.Utilities;

public class SceneTransition : MonoBehaviour
{
    public float startDelay = 0.5f;
    public float endDelay = 1f;
    public float rotationTime = 3f;
    public float increment = 0.2f;

    public AnimationCurve rotationCurve;

    [Header("References")]
    public RectTransform subObject;

    [Header("Debug Params")]
    public float debugStartRotation;
    public float debugEndRotation;
    public float debugRotationTime;
    bool isDebugging;

    float timePassed = 0f;
    float endRotation;
    float startRotation;
    bool isFading = false;

    private void Start()
    {
        timePassed = 0f;

        // Set fading
        isFading = true;
        StartCoroutine(Fader.FadeOut(1f, delegate { isFading = false; }));

        // DEBUG STUFF
        if (!InitialLevel.firstSceneLoaded)
        {
            // Use debug stuff instead
            startRotation = debugStartRotation;
            endRotation = debugEndRotation;
            rotationTime = debugRotationTime;
            isDebugging = true;
            return;
        }

        // Set initial rotation
        startRotation = GlyphManager.oldTimeIndex * 90;
        subObject.rotation = Quaternion.Euler(0, 0, startRotation);

        // Get amount of complete rotations needed
        int completeRotations = (GlyphManager.GetIndex(GlyphManager.landscape) + GlyphManager.GetIndex(GlyphManager.biome)) / 4;
        rotationTime += completeRotations * increment;

        if (GlyphManager.timeIndex < GlyphManager.oldTimeIndex)
            completeRotations += 1;

        // Set rotation that it needs to reach
        endRotation = GlyphManager.timeIndex * 90 + completeRotations * 360;
    }

    private void Update()
    {
        if (isFading)
            return;

        timePassed += Time.deltaTime;

        if (timePassed > startDelay + rotationTime)
        {
            // Change scene
            isFading = true;
            subObject.rotation = Quaternion.Euler(0, 0, endRotation);
            if (!isDebugging)
                StartCoroutine(Fader.FadeIn(1f, delegate { SceneManager.LoadScene(GlyphManager.landscape.sceneName); }));
            return;
        }

        if (timePassed > startDelay && timePassed < startDelay + rotationTime)
        {
            float t = (timePassed).Remap(startDelay, startDelay + rotationTime, 0f, 1f);
            float v = rotationCurve.Evaluate(t).Remap(0f, 1f, startRotation, endRotation);
            subObject.rotation = Quaternion.Euler(0, 0, v % 360);
        }
    }
}
