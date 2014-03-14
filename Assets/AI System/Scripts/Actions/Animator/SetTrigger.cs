using UnityEngine;
using System.Collections;

namespace AISystem.Actions.Animator{
	[Category("Animator")]
	[System.Serializable]
	public class SetTrigger : BaseAction {
		[AnimatorParameterAttribute(AnimatorParameter.Trigger)]
		public string parameterName;

		private UnityEngine.Animator animator;
		private int hash;
		
		public override void OnAwake (){
			animator = owner.GetComponent<UnityEngine.Animator> ();
			hash = UnityEngine.Animator.StringToHash (parameterName);
		}

		public override void OnEnter ()
		{
			animator.SetTrigger (hash);
			Finish ();
		}
	}
}