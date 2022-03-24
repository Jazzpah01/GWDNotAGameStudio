using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName ="Assets/Glyph/Landscape")]
public class GlyphLandscape : Glyph
{
    public string sceneName;
    public List<Location> locations; // Temp until we get a good system
}