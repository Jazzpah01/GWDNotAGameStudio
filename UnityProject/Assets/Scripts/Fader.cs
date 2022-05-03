using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JStuff.Utilities;

public class Fader : MonoBehaviour
{
    public Image image;
    public static Fader instance;

    private void Awake()
    {
        instance = this;
    }

    public void Fade(float v)
    {
        Color c = image.color;
        c.a = 1 - v;
        image.color = c;
    }

    public static IEnumerator FadeIn(float time, Action callback = null)
    {
        float ctime = time;
        float realTime = Time.time;

        SetFade(0f);

        while (ctime > 0f)
        {
            SetFade(ctime.Remap(time, 0f, 0f, 1f));
            ctime -= Time.time - realTime;
            realTime = Time.time;
            yield return null;
        }

        SetFade(1f);

        if (callback != null)
            callback();
    }

    public static IEnumerator FadeOut(float time, Action callback = null)
    {
        float ctime = time;
        float realTime = Time.time;

        SetFade(1f);

        while (ctime > 0f)
        {
            SetFade(ctime.Remap(time, 0f, 1f, 0f));
            ctime -= Time.time - realTime;
            realTime = Time.time;
            yield return null;
        }

        SetFade(0f);

        if (callback != null)
            callback();
    }

    public static void SetFade(float v)
    {
        Color c = instance.image.color;
        c.a = v;
        instance.image.color = c;
    }
}
