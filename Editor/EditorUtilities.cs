using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace EditorUtilities 
{
    public static class EditorFunc {
        public static void LayoutFieldWithLabel(string label, ref int field) {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label);
            field = EditorGUILayout.IntField(field);
            GUILayout.EndHorizontal();
        }
        public static void LayoutFieldWithLabel(string label, ref float field) {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label);
            field = EditorGUILayout.FloatField(field);
            GUILayout.EndHorizontal();
        }
        public static void LayoutFieldWithLabel(string label, ref Vector3 field) {
            field = EditorGUILayout.Vector3Field(label, field);
        }
        public static void LayoutFieldWithLabel(string label, ref bool field) {
            field = EditorGUILayout.Toggle(label, field);
        }
    }
}
