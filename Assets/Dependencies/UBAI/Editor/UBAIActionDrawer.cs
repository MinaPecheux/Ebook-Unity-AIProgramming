/* Adapted from:
 * https://learn.unity.com/tutorial/property-drawers-and-custom-inspectors */
using UnityEditor;
using UnityEngine;

namespace UBAI
{

    [CustomPropertyDrawer(typeof(UBAIAction))]
    public class UBAIActionDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // use the default property height, which takes into account the expanded state
            return EditorGUIUtility.singleLineHeight +
                EditorGUI.GetPropertyHeight(property.FindPropertyRelative("vetoes"), true) +
                EditorGUIUtility.standardVerticalSpacing;
        }

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Don't make child fields be indented
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            SerializedProperty vetos = property.FindPropertyRelative("vetoes");

            // Calculate rects
            float rowHeight = EditorGUIUtility.singleLineHeight;
            float vetoesHeight = EditorGUI.GetPropertyHeight(vetos, true);
            float utilityWidth = position.width * 0.6f;
            float funcWidth = position.width * 0.4f;
            Rect utilityRect = new Rect(position.x, position.y, utilityWidth, rowHeight);
            Rect funcRect = new Rect(position.x + utilityWidth + 5, position.y, funcWidth, rowHeight);
            Rect vetoesRect = new Rect(
                position.x, position.y + EditorGUIUtility.standardVerticalSpacing + rowHeight,
                position.width, vetoesHeight);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(utilityRect, property.FindPropertyRelative("utility"), GUIContent.none);
            EditorGUI.PropertyField(funcRect, property.FindPropertyRelative("func"), GUIContent.none);
            EditorGUI.PropertyField(vetoesRect, vetos, true);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }

}
