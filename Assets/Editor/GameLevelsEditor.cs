using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameLevelsView))]
public class GameLevelsEditor : Editor {

    public override void OnInspectorGUI()
    {
        GameLevelsView gameLevels = (GameLevelsView)target;

        // 繪製預設 Inspector
        // DrawDefaultInspector(); 

        // 序列物件，只要是繪製指定成員就要加入
        serializedObject.Update(); 

        // 透過Editor API 繪製成員
        // EditorGUILayout.PropertyField(serializedObject.FindProperty("m_levels"), true);

        // 自行定義方法繪製成員
        Show(serializedObject.FindProperty("m_levels"), gameLevels);

        serializedObject.ApplyModifiedProperties();
    }

    // 自定義
    public void Show(SerializedProperty list, GameLevelsView gameLevels)
    {
        // 顯示我的清單
        EditorGUILayout.PropertyField(list);

        // 顯示清單大小
        EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));

        if (list.isExpanded)
        {
            EditorGUI.indentLevel += 1;
            for (int i = 0; i < list.arraySize; i++)
            {
                GUI.backgroundColor = Color.white;

                // 顯示清單元素
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));

                EditorGUI.indentLevel += 1;
                if (list.GetArrayElementAtIndex(i).isExpanded)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);

                    // 顯示清單元素成員
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("center"));
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("cameraSize"));

                    // 水平布局
                    EditorGUILayout.BeginHorizontal();
                    // 建立按鈕
                    if (GUILayout.Button("Level" + (i + 1) + " View"))
                    {
                        GameLevelsView.ChangeCameraView(gameLevels.m_levels[i]);
                    }
                    
                    GUI.backgroundColor = Color.green;
                    if (GUILayout.Button("Set View"))
                    {
                        GameLevelsView.SetCameraView(gameLevels.m_levels[i]);
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.EndVertical();
                }

                EditorGUI.indentLevel -= 1;
            }
            EditorGUI.indentLevel -= 1;
        }
    }

}
