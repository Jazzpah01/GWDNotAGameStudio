using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(LocationData))]
public class LocationsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LocationData o = (LocationData)serializedObject.targetObject;

        if (GUILayout.Button("Add Landscape"))
        {
            LocationLandscapeAxis newob = ScriptableObject.CreateInstance<LocationLandscapeAxis>();
            AssetDatabase.AddObjectToAsset(newob, o);

            o.landscapeAxis.Add(newob);

            AssetDatabase.SaveAssets();
        }

        if (GUILayout.Button("Add Biome"))
        {
            foreach (LocationLandscapeAxis item in o.landscapeAxis)
            {
                //item.biomeAxis.Add(ScriptableObject.CreateInstance<Location>());
            }
        }

        for (int j = 0; j < o.LandscapeAxisWidth; j++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < o.BiomeAxisWidth; i++)
            {
                //o[i, j] = (Location)EditorGUILayout.ObjectField(o[i, j], o.GetType(), false);
                SerializeObject(o[i, j]);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    /// <summary>
    /// this is one more "low level" and all GUILayout stuff must be done by hand
    /// </summary>
    void SerializeObject(object objectToDraw)
    {
        Type te = objectToDraw.GetType();
        System.Reflection.FieldInfo[] pi = te.GetFields();
        //Debug.Log(pi.Length);

        foreach (System.Reflection.FieldInfo p in pi)
        {
            EditorGUILayout.BeginHorizontal();

            Type pType = p.GetValue(objectToDraw).GetType();

            EditorGUILayout.LabelField(pType.ToString());
            EditorGUILayout.LabelField(p.Name);
            EditorGUILayout.LabelField(p.GetValue(objectToDraw).ToString());

            EditorGUILayout.EndHorizontal();
        }
    }
}