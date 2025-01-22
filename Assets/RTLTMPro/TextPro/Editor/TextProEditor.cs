using YoYo.UI;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;


[CanEditMultipleObjects]
[CustomEditor(typeof(TextPro), true)]
public class TextProEditor : Editor
{
    TextPro textPro;
    private bool foldout;

    void OnEnable()
    {
        //base.OnEnable();
        //DisableDefaultInspector();

        textPro = (TextPro)target;
        textPro.FindRequiredComponent();
        UnityEditorInternal.ComponentUtility.MoveComponentUp(textPro);
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_text"), new GUIContent("Text"));

        EditorGUILayout.Space();

        //foldout = EditorGUILayout.Foldout(foldout, "RTL Settings", true);

        //if (foldout)
        //{
        //    EditorGUILayout.BeginHorizontal();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_numberMode"), new GUIContent("Number Mode"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("FontWeight"), new GUIContent("Font Weight"));
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("forceFix"), new GUIContent("Force Fix"));

        //    EditorGUILayout.EndHorizontal();
        //}



        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            textPro.SetText();
        }

        //EditorGUILayout.PropertyField(serializedObject.FindProperty("storedTexts"), new GUIContent("Localization"), true);

        if (GUILayout.Button("ReFix"))
        {
            serializedObject.ApplyModifiedProperties();
            textPro.SetText();
            EditorUtility.SetDirty(target);
        }
    }
}
