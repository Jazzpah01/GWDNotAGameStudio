using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlaceManager))]
public class PlaceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlaceManager obj = (PlaceManager)serializedObject.targetObject;

        if (GUILayout.Button("Load Place"))
        {
            obj.LoadPlace();
        }

        if (GUILayout.Button("Save Place"))
        {
            SavePlace(obj);
        }
    }

    public static void SavePlace(PlaceManager placeManager)
    {
        GlyphLandscape landscape = placeManager.levelManager.landscapeGlyph;

        if (landscape == null || placeManager.biome == null || placeManager.time == null)
        {
            throw new System.Exception("ERROR: Cannot save place when some of the glyphs are not specified.");
        }

        placeManager.SetupLocalPlaces();

        int x = GlyphManager.GetIndex(landscape);
        int y = GlyphManager.GetIndex(placeManager.biome);
        int z = GlyphManager.GetIndex(placeManager.time);

        if (placeManager.currentPlace == null)
        {
            placeManager.currentPlace = placeManager.InitializePlace();
        }

        placeManager.currentPlace.name = $"Place of ({x},{y},{z})";
        PlacesEditor.SavePlacePrefab(placeManager.places, x, y, z, placeManager.currentPlace);

        placeManager.CleanupScene();
    }
}