using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ExtendedEditorWindow : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty currentProperty;

    protected void DrawProperties(SerializedProperty properties, bool drawChildren) {
        string lastPropPath = "";
        foreach(SerializedProperty p in properties) {
            if(p.isArray && p.propertyType == SerializedPropertyType.Generic) {
                EditorGUILayout.BeginHorizontal();
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                EditorGUILayout.EndHorizontal();

                if (p.isExpanded) {
                    EditorGUI.indentLevel++;
                    DrawProperties(p, drawChildren);
                    EditorGUI.indentLevel--;
                }
            }
            else {
                if (!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath)) { continue; }
                lastPropPath = p.propertyPath;
                EditorGUILayout.PropertyField(p, drawChildren);
            }
        }
    }
    protected void Apply() {
        serializedObject.ApplyModifiedProperties();
    }
}
