using UnityEngine;
using System.Collections;

namespace AISystem.Actions.Animator{
	[Category("Animator")]
	[System.Serializable]
	public class Play : BaseAction {
		public string stateName;
		public int layer;

		private UnityEngine.Animator animator;
		private int hash;
		
		public override void OnAwake (){
			animator = owner.GetComponent<UnityEngine.Animator> ();
			hash = UnityEngine.Animator.StringToHash (stateName);
		}

		public override void OnEnter ()
		{
			animator.Play (hash, layer);
			Finish ();
		}
	}
}