using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SankusaLib.CustomPopupLib;
using System.Linq;

namespace Sankusa.unity1week202303.Domain
{
    [CustomPropertyDrawer(typeof(HumanParameterIdAttribute))]
    public class HumanParameterIdDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label)
        {
            if(serializedProperty.propertyType == SerializedPropertyType.String)
            {
                serializedProperty.stringValue = CustomPopup.PopupFromScriptableObject<HumanParameterMaster>(rect, serializedProperty.stringValue, x => x.ParameterDataList.Select(x => x.ParameterId));
            }
            else
            {
                EditorGUI.PropertyField(rect, serializedProperty, GUIContent.none);
            }
        }
    }
}