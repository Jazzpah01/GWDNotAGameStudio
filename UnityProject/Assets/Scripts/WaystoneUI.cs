using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaystoneUI : MonoBehaviour
{
    public GameObject playerGlyphs;
    public GameObject waystoneGlyphs;
    public GameObject submitButton;

    public static WaystoneUI instance;

    private void Awake()
    {
        instance = this;
    }
}