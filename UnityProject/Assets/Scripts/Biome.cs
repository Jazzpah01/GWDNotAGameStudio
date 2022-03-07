using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Assets/Glyph/Biome")]
public class Biome : Glyph
{
    public float spawnrate = 1;
    public List<TerrainObject> foreGround;
}