using UnityEngine;
using System.Collections;
using System;

public class UseGlobalPrameterAttribute : PropertyAttribute {
	public string globalField;
	public Type[] parameterType;

	public UseGlobalPrameterAttribute(string globalField, params Type[] parameterType){
		this.parameterType = parameterType;
		this.globalField = globalField;
	}
}
