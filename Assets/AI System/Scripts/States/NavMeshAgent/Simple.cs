using UnityEngine;
using System.Collections;

namespace AISystem.States.NavMeshAgent{
	[Category("NavMeshAgent")]
	[System.Serializable]
	public class Simple : State {
		public bool applyRootMotion;
		protected Animator animator;
		protected UnityEngine.NavMeshAgent agent;
		public override void OnAwake ()
		{
			agent = owner.GetComponent<UnityEngine.NavMeshAgent> ();
			animator = owner.GetComponent<Animator> ();
		}

		public override void OnAnimatorMove ()
		{
			agent.updateRotation = !applyRootMotion;
			if (applyRootMotion) {
				owner.transform.rotation=animator.rootRotation;
				if(agent != null){
					agent.velocity = animator.deltaPosition / Time.deltaTime;
				}
			}
		}
	}
}