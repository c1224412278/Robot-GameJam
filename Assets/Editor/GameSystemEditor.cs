using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameSystem))]
public class GameSystemEditor : Editor {

    public override void OnInspectorGUI()
    {
        GameSystem gameSystem = (GameSystem)target;

        DrawDefaultInspector();

        serializedObject.Update();

        // EditorGUILayout.PropertyField(serializedObject.FindProperty("m_LV"), true);

        for (int i = 0; i < gameSystem.m_MaxLV; i++)
        {
            if(GUILayout.Button("Level " + i.ToString()))
            {
                gameSystem.Fn_NextLevelEvent(i);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

}
