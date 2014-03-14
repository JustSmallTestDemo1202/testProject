using UnityEngine;
using System.Collections;
using AISystem;

public static class ParameterExtensions {

	public static GameObject GetGameObject(this AIRuntimeController controller,string name){
		GameObjectParameter param = (GameObjectParameter)controller.GetParameter (name);
		return (param != null ? param.Value : null);
	}

	public static float GetFloat(this AIRuntimeController controller,string name){
		FloatParameter param = (FloatParameter)controller.GetParameter (name);
		return (param != null ? param.Value : 0);
	}

	public static void SetFloat(this AIRuntimeController controller,string name,float value){
		FloatParameter param = (FloatParameter)controller.GetParameter (name);
		param.Value = value;
	}

	public static int GetInt(this AIRuntimeController controller,string name){
		IntParameter param = (IntParameter)controller.GetParameter (name);
		return (param != null ? param.Value : 0);
	}
	public static void SetInt(this AIRuntimeController controller,string name,int value){
		IntParameter param = (IntParameter)controller.GetParameter (name);
		param.Value = value;
	}

	public static float GetFloatOrInt(this AIRuntimeController controller,string name){
		NamedParameter param = controller.GetParameter (name);
		if (param != null) {
			if (param is FloatParameter) {
				return controller.GetFloat (name);
			} else {
				return controller.GetInt (name);
			}
		}
		Debug.Log ("null");
		return 0;
	}

}
