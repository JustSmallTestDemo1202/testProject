using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(HideAttribute))]
public class HideAttributeDrawer : PropertyDrawer {
	private HideAttribute hideAttribute{
		get{
			return (HideAttribute)attribute;
		}
	}

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		SerializedProperty prop = property.serializedObject.FindProperty (hideAttribute.boolField);
		position.x += 4;
		if (prop != null && !prop.boolValue) {
			EditorGUI.PropertyField(position,property,label);
		}
	}

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		SerializedProperty prop = property.serializedObject.FindProperty (hideAttribute.boolField);
		return (prop != null && !prop.boolValue)?base.GetPropertyHeight(property,label):0;
	}
}
