using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSprite : MonoBehaviour
{
    void Start()
    {
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(this);
    }
}