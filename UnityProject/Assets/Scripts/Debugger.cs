using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public static bool Debug = false;

    public bool debug = false;

    private void Awake()
    {
        Debug = debug;
    }
}