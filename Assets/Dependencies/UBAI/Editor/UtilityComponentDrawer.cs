/* Adapted from:
 * https://learn.unity.com/tutorial/property-drawers-and-custom-inspectors */
using UnityEditor;
using UnityEngine;

namespace UBAI
{

    [CustomPropertyDrawer(typeof(UtilityComponent))]
    public class UtilityComponentDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Don't make child fields be indented
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            float weightWidth = position.width * 0.2f;
            float varWidth = position.width * 0.8f;
            Rect weightRect = new Rect(position.x, position.y, weightWidth, position.height);
            Rect varRect = new Rect(position.x + weightWidth + 5, position.y, varWidth, position.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(varRect, property.FindPropertyRelative("variable"), GUIContent.none);
            EditorGUI.PropertyField(weightRect, property.FindPropertyRelative("weight"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }

}
