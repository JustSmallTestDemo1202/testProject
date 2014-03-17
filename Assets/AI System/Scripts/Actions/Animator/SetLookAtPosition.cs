using UnityEngine;
using System.Collections;

namespace AISystem.Actions.Animator{
	[Category("Animator")]
	[System.Serializable]
	public class SetLookAtPosition : BaseAction {
		public float weight;
		public float bodyWeight;
		public float headWeight=1.0f;
		public float eyesWeight;	
		public float clampWeight=0.5f;
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
			position = useGlobal ? ((GameObjectParameter)owner.GetParameter (globalPosition)).Value.transform.position : position;
			animator.SetLookAtWeight (weight, bodyWeight, headWeight, eyesWeight, clampWeight);
			animator.SetLookAtPosition (position);
			Finish ();
		}
	}
}