using UnityEngine;
using System.Collections;

public class HideAttribute : PropertyAttribute {
	public string boolField;

	public HideAttribute(string boolField){
		this.boolField = boolField;
	}

	public HideAttribute(){
	}
}
