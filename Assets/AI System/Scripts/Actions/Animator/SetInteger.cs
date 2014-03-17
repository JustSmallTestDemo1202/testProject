using UnityEngine;
using System.Collections;

namespace AISystem.Actions.Animator{
	[Category("Animator")]
	[System.Serializable]
	public class SetInteger : BaseAction {
		[AnimatorParameterAttribute(AnimatorParameter.Int)]
		public string parameterName;
		public int value;

		private UnityEngine.Animator animator;
		private int hash;
		
		public override void OnAwake (){
			animator = owner.GetComponent<UnityEngine.Animator> ();
			hash = UnityEngine.Animator.StringToHash (parameterName);
		}
		
		public override void OnEnter ()
		{
			animator.SetInteger (hash, value);
			Finish ();
		}
	}
}