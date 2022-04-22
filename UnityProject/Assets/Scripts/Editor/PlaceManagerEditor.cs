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
            obj.SavePlace();
        }

        if (GUILayout.Button("Cleanup Scene"))
        {
            obj.CleanupScene();
        }
    }
}