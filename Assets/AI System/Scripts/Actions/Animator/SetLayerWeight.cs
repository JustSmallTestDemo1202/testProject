using UnityEngine;
using System.Collections;

namespace AISystem.Actions.Animator{
	[Category("Animator")]
	[System.Serializable]
	public class SetLayerWeight : BaseAction {
		public int layer;
		public float weight;

		private UnityEngine.Animator animator;
		public override void OnAwake (){
			animator = owner.GetComponent<UnityEngine.Animator> ();
		}
		
		public override void OnEnter ()
		{
			animator.SetLayerWeight (layer, weight);
			Finish ();
		}
	}
}