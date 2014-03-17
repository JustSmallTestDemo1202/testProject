using UnityEngine;
using System.Collections;
using System;

public class StoreParameterAttribute:PropertyAttribute {
	public string _default="None";
	public bool required;
	public Type[] types;

	public StoreParameterAttribute(bool required,string _default,params Type[] types){
		this.required = required;
		this.types = types;
		this._default = _default;
	}

	public StoreParameterAttribute(bool required,params Type[] types){
		this.required = required;
		this.types = types;
	}
}
