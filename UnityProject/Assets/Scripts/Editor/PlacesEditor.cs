using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Places))]
public class PlacesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Places obj = (Places)serializedObject.targetObject;

        if (!obj.initialized)
        {
            if (GUILayout.Button("Initialize"))
            {
                EditorUtility.SetDirty(obj);
                obj.InitializeArray();
            }
        }
        if (obj.initialized && obj.places != null && obj.glyphCollection != null && obj.lengthOfX * obj.lengthOfY * obj.lengthOfZ != obj.places.Length)
        {
            GUILayout.Label("ERROR: Current length of place array does not equal dimensions!");
            if (GUILayout.Button("Resize"))
            {
                obj.RestructureDimensions(obj.lengthOfX, obj.lengthOfY, obj.lengthOfZ);
            }
        }
        if (obj.initialized)
        {
            if (GUILayout.Button("Manual Save"))
            {
                obj.ManualSave();
            }
            if (GUILayout.Button("Log objects"))
            {
                Debug.Log(obj.ToString());
            }
        }
    }
}