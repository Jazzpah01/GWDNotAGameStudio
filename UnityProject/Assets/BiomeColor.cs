using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeColor : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().color = GlyphManager.biome.roadColor;
    }
}
