using UnityEngine;
using System.Collections;

namespace AISystem.Actions.Animator{
	[Category("Animator")]
	[System.Serializable]
	public class SetBool : BaseAction {
		[AnimatorParameterAttribute(AnimatorParameter.Bool)]
		public string parameterName;
		public bool value;

		private UnityEngine.Animator animator;
		private int hash;
		
		public override void OnAwake (){
			animator = owner.GetComponent<UnityEngine.Animator> ();
			hash = UnityEngine.Animator.StringToHash (parameterName);
		}
		
		public override void OnEnter ()
		{
			animator.SetBool (hash, value);
			Finish ();
		}
	}
}