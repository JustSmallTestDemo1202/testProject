using UnityEngine;
using System.Collections;

namespace AISystem.Actions.Animator{
	[Category("Animator")]
	[System.Serializable]
	public class SetIKPosition : BaseAction {
		public AvatarIKGoal goal;
		public float weight;
		[UseGlobalPrameter("globalPosition",typeof(GameObjectParameter))]
		public bool useGlobal;
		[Hide]
		public string globalPosition;
		[Hide("useGlobal")]
		public Vector3 position;
	
		private UnityEngine.Animator animator;
		
		public override void OnAwake (){
			animator = owner.GetComponent<UnityEngine.Animator> ();
		}
		
		public override void OnAnimatorIK (int layerIndex)
		{
			position = (useGlobal ? ((GameObjectParameter)owner.GetParameter (globalPosition)).Value.transform.position : position);
			animator.SetIKPositionWeight (goal, weight);
			animator.SetIKPosition (goal, position);
			Finish ();
		}
	}
}