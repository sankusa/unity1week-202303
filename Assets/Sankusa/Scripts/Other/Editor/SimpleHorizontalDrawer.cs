using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SankusaLib.EditorLib;

namespace Sankusa.unity1week202303 {
    [CustomPropertyDrawer(typeof(SimpleHorizontalDrawerAttribute))]
    public class SimpleHorizontalDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ExEditorGUI.ChildsPropertyField(position, property, Orientation.Horizontal);
        }
    }
}