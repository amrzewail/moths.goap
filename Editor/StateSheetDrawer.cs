using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GOAP.Editor
{
    [CustomPropertyDrawer(typeof(StateSheet))]
    public class StateSheetDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            var dict = property.FindPropertyRelative("_properties");

            Debug.Log(dict);

        }
    }
}