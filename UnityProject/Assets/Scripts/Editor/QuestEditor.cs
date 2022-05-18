using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Quest quest = (Quest)serializedObject.targetObject;

        if (Application.isPlaying && Application.isEditor)
        {
            GUILayout.Label($"---DEBUG---");
            GUILayout.Label($"Quest Index: {quest.QuestIndex}");
            GUILayout.Label($"---     ---");
        }

        quest.customEditing = EditorGUILayout.Toggle("Enable Custom Editing", quest.customEditing);

        if (!quest.customEditing)
        {
            base.OnInspectorGUI();
            return;
        }

        if (GUILayout.Button("Save Asset"))
        {
            foreach (QuestEvent item in quest.questEvents)
            {
                EditorUtility.SetDirty(item);
            }
            EditorUtility.SetDirty(quest);
            AssetDatabase.SaveAssets();
        }

        if (GUILayout.Button("Sort"))
        {
            quest.questEvents.Sort();
        }

        if (quest.questEvents != null)
        {
            for (int i = 0; i < quest.questEvents.Count; i++)
            {
                EditorGUILayout.BeginFoldoutHeaderGroup(true, $"Quest Event {i}");

                QuestEventType actualType = quest.questEvents[i].questEventType;
                QuestEventType type = (QuestEventType)EditorGUILayout.EnumPopup(actualType);

                if (actualType != type)
                {
                    Debug.Log("New type!!!");
                    QuestEvent oldAsset = quest.questEvents[i];
                    quest.questEvents[i] = CreateNewEvent(type);
                    quest.questEvents[i].name = type.ToString();
                    AssetDatabase.RemoveObjectFromAsset(oldAsset);
                    AssetDatabase.AddObjectToAsset(quest.questEvents[i], quest);
                    EditorUtility.SetDirty(quest.questEvents[i]);
                }

                if (GUILayout.Button("Delete Event"))
                {
                    // Event is deleted
                    QuestEvent oldAsset = quest.questEvents[i];
                    quest.questEvents.Remove(oldAsset);
                    AssetDatabase.RemoveObjectFromAsset(oldAsset);
                    AssetDatabase.SaveAssets();
                } else
                {
                    // Event is not deleted
                    bool changed = SerializeObject(quest.questEvents[i]);
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                EditorGUILayout.Separator();
            }
        }

        if (GUILayout.Button("Add Quest Event"))
        {
            QuestEvent eventt = CreateNewEvent(QuestEventType.EnterScene);
            eventt.name = QuestEventType.EnterScene.ToString();
            EditorUtility.SetDirty(eventt);
            EditorUtility.SetDirty(quest);
            AssetDatabase.AddObjectToAsset(eventt, quest);

            quest.questEvents.Add(eventt);
            AssetDatabase.SaveAssetIfDirty(quest);
        }
    }

    bool SerializeObject(object objectToDraw)
    {
        if (objectToDraw == null)
        {
            return false;
        }

        Type te = objectToDraw.GetType();
        System.Reflection.FieldInfo[] pi = te.GetFields();

        if (pi == null)
        {
            return false;
        }

        bool changesMade = false;

        foreach (System.Reflection.FieldInfo p in pi)
        {
            if (p == null)
            {
                Debug.Log("P is null?");
                continue;
            }

            EditorGUILayout.BeginHorizontal();

            Type pType = p.FieldType;
            object o = p.GetValue(objectToDraw);

            EditorGUILayout.LabelField(p.Name);
            
            if (pType == typeof(int))
            {
                p.SetValue(objectToDraw, EditorGUILayout.IntField((int)p.GetValue(objectToDraw)));
            } else if (pType == typeof(float))
            {
                p.SetValue(objectToDraw, EditorGUILayout.FloatField((float)p.GetValue(objectToDraw)));
            } else if (pType == typeof(string))
            {
                p.SetValue(objectToDraw, EditorGUILayout.TextField((string)p.GetValue(objectToDraw)));
            }
            else if (pType == typeof(bool))
            {
                p.SetValue(objectToDraw, EditorGUILayout.Toggle((bool)p.GetValue(objectToDraw)));
            }
            else if (pType.IsSubclassOf(typeof(UnityEngine.Object)))
            {
                p.SetValue(objectToDraw, EditorGUILayout.ObjectField((UnityEngine.Object)p.GetValue(objectToDraw), pType, false));
            } else if (pType is null)
            {
                p.SetValue(objectToDraw, EditorGUILayout.ObjectField((UnityEngine.Object)p.GetValue(objectToDraw), pType, false));
            }
            else if (pType.IsEnum)
            {
                p.SetValue(objectToDraw, EditorGUILayout.EnumFlagsField((Enum)p.GetValue(objectToDraw)));
            }
            else if (pType == typeof(Vector2))
            {
                p.SetValue(objectToDraw, EditorGUILayout.Vector2Field("", (Vector2)p.GetValue(objectToDraw)));
            }
            else
            {
                throw new System.Exception("Could not serialize object!");
            }

            object newO = p.GetValue(objectToDraw);

            if (newO != o)
            {
                changesMade = true;
            }

            EditorGUILayout.EndHorizontal();
        }
        return changesMade;
    }

    private QuestEvent CreateNewEvent(QuestEventType type)
    {
        switch (type)
        {
            case QuestEventType.EnterScene:
                return CreateInstance<EnterSceneEvent>();
            case QuestEventType.SetDialogue:
                return CreateInstance<SetDialogueEvent>();
            case QuestEventType.SpawnGameObject:
                return CreateInstance<SpawnGameObjectEvent>();
            case QuestEventType.RemoveGameObject:
                return CreateInstance<RemoveGameObjectEvent>();
            case QuestEventType.GiveGlyph:
                return CreateInstance<GiveGlyphEvent>();
            case QuestEventType.SetInteractable:
                return CreateInstance<InteractEvent>();
            case QuestEventType.StartQuest:
                return CreateInstance<StartQuestEvent>();
            case QuestEventType.StartDialogue:
                return CreateInstance<StartDialogueEvent>();
            case QuestEventType.Move:
                return CreateInstance<MoveQuestEvent>();
            case QuestEventType.InstantMove:
                return CreateInstance<InstantMoveEvent>();
            default:
                throw new Exception("Event isn't implemented!");
        }
    }




    //void SerializeObject(object objectToDraw)
    //{
    //    Type te = objectToDraw.GetType();
    //    System.Reflection.FieldInfo[] pi = te.GetFields();
    //    //Debug.Log(pi.Length);

    //    foreach (System.Reflection.FieldInfo p in pi)
    //    {
    //        EditorGUILayout.BeginHorizontal();

    //        Type pType = p.GetValue(objectToDraw).GetType();

    //        EditorGUILayout.LabelField(pType.ToString());
    //        EditorGUILayout.LabelField(p.Name);
    //        EditorGUILayout.LabelField(p.GetValue(objectToDraw).ToString());

    //        EditorGUILayout.EndHorizontal();
    //    }
    //}
}
