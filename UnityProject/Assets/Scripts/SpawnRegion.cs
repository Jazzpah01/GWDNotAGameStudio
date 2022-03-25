using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRegion : Region
{
    public enum Layer
    {
        InFrontOfPlayer,
        BehindPlayer
    }


    public float spawnrate = 1;
    public float buttomScale = 1;
    public float topScale = 1;
    public Layer sortingLayer = Layer.BehindPlayer;
    public int sortingOrder = 0;
}