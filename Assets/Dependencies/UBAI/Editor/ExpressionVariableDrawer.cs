/* Adapted from:
 * https://learn.unity.com/tutorial/property-drawers-and-custom-inspectors */
using UnityEditor;
using UnityEngine;

namespace UBAI
{

    [CustomPropertyDrawer(typeof(ExpressionVariable))]
    public class ExpressionVariableDrawer : PropertyDrawer
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
            float nameWidth = position.width * 0.4f;
            float varWidth = position.width * 0.6f;
            Rect nameRect = new Rect(position.x, position.y, nameWidth, position.height - 2);
            Rect varRect = new Rect(position.x + nameWidth + 5, position.y, varWidth, position.height - 2);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);
            EditorGUI.PropertyField(varRect, property.FindPropertyRelative("variable"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }

}
