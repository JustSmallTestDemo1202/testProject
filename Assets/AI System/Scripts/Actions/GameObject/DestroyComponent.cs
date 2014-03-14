using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("GameObject")]
	[System.Serializable]
	public class DestroyComponent : BaseAction {
		public string component;
		public float delay;
		
		public override void OnEnter ()
		{
			GameObject.Destroy (owner.GetComponent(component), delay);
			Finish ();
		}
	}
}