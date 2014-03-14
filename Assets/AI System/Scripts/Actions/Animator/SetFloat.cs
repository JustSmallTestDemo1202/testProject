using UnityEngine;
using System.Collections;

namespace AISystem.Actions.Animator{
	[Category("Animator")]
	[System.Serializable]
	public class SetFloat : BaseAction {
		[AnimatorParameter(AnimatorParameter.Float)]
		public string parameterName;

		[UseGlobalPrameter("globalFloat",typeof(FloatParameter),typeof(IntParameter))]
		public bool useGlobal;
		[Hide]
		public string globalFloat;
		[Hide("useGlobal")]
		public float value;
		public float dampTime=0.15f;

		private UnityEngine.Animator animator;
		private int hash;

		public override void OnAwake (){
			animator = owner.GetComponent<UnityEngine.Animator> ();
			hash = UnityEngine.Animator.StringToHash (parameterName);
		}

		public override void OnUpdate ()
		{

			float mValue = useGlobal ? owner.GetFloatOrInt(globalFloat) : value;
			if (dampTime > 0) {
				animator.SetFloat (hash, mValue, dampTime, Time.deltaTime);
			} else {
				animator.SetFloat (hash, mValue);
			}
			Finish ();
		}
		
	}
}