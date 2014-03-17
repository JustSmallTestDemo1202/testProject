using UnityEngine;
using UnityEditor;
using System.Collections;
using AISystem;

[CustomPropertyDrawer(typeof(UseGlobalPrameterAttribute))]
public class UseGlobalPrameterDrawer : PropertyDrawer {
	private UseGlobalPrameterAttribute useGlobalAttribute{
		get{
			return (UseGlobalPrameterAttribute)attribute;
		}
	}

	private AIController controller;
	private bool initialized;
	private int selectedIndex;
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		if (!initialized) {
			AIEditorWindow[] windows=Resources.FindObjectsOfTypeAll<AIEditorWindow>();
			if(windows.Length >0){
				controller = windows[0].controller;
			}
			initialized=true;
		}
		position.x += 4;
		position.width -= 4;
		position.height *= 0.5f;
		EditorGUI.PropertyField(position,property,label);
		position.y += position.height;
		bool enabled = property.boolValue;
		if (enabled && controller != null) {
			
			SerializedProperty globalProperty = property.serializedObject.FindProperty (useGlobalAttribute.globalField);
			string[] parameters = controller.GetParameterNames (useGlobalAttribute.parameterType);
			
			if (parameters.Length == 0) {
				System.Array.Resize (ref parameters, parameters.Length + 1);
				parameters [parameters.Length - 1] = "None";
			}
			
			for (int i=0; i< parameters.Length; i++) {
				if (parameters [i] == globalProperty.stringValue) {
					selectedIndex = i;
				}
			}

			if (parameters.Length > 0) {
				GUI.color=(globalProperty.stringValue == "None"?Color.red:Color.white);
				selectedIndex = EditorGUI.Popup (position, "Value", selectedIndex, parameters);
				GUI.color=Color.white;
			}
			globalProperty.stringValue = parameters [selectedIndex];
		} 
	}

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight (property, label)*(property.boolValue?2:1);
	}
}
