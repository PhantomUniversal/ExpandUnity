using UnityEditor;
using UnityEngine;

namespace PhantomEngine
{
    public class VariablePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            VariableAttribute variableAttribute = (VariableAttribute)attribute;
            label.text = variableAttribute.variable;
            EditorGUI.PropertyField(position, property, label);
        }
    }
}