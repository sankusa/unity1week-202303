using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SankusaLib.CustomPopupLib;
using System.Linq;
using Sankusa.unity1week202303.Presentation;

namespace Sankusa.unity1week202303.Domain
{
    [CustomPropertyDrawer(typeof(CommandIdAttribute))]
    public class CommandIdDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label)
        {
            if(serializedProperty.propertyType == SerializedPropertyType.String)
            {
                serializedProperty.stringValue = CustomPopup.PopupFromScriptableObject<CommandMaster>(rect, serializedProperty.stringValue, x => x.Commands.Select(x => x.CommandId));
            }
            else
            {
                EditorGUI.PropertyField(rect, serializedProperty, GUIContent.none);
            }
        }
    }
}