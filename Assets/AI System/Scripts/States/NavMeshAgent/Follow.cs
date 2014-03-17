using UnityEngine;
using System.Collections;

namespace AISystem.States.NavMeshAgent{
	[Category("NavMeshAgent")]
	[System.Serializable]
	public class Follow : Simple {
		[StoreParameter(true,typeof(GameObjectParameter))]
		public string target;
		public float speed=2.0f;
		public float rotation=150.0f;

		protected GameObject mTarget;

		public override void OnUpdate ()
		{
			if (agent.isOnOffMeshLink) {
				DoJump();
			}else{
				if (mTarget != null) {
					agent.SetDestination (mTarget.transform.position);
				}
			}
		}
		
		public override void OnEnter ()
		{
			agent.speed = speed;
			agent.angularSpeed = rotation;
			agent.autoTraverseOffMeshLink = false;
			mTarget = ((GameObjectParameter)owner.GetParameter (target)).Value;
		}
		
		public override void OnExit ()
		{
			agent.Stop ();
			agent.SetDestination(owner.transform.position);
		}

		[AnimatorParameter(AnimatorParameter.State)]
		public string jumpState;
		private bool traversingLink;
		private OffMeshLinkData currLink;
		private Vector3 start;
		private Vector3 end;

		public void DoJump(){
			
			agent.Stop(true);
			
			if (!traversingLink)
			{
				animator.CrossFade(jumpState,0);
				currLink = agent.currentOffMeshLinkData;
			 	start=agent.transform.position;
				end=currLink.endPos;
				NavMeshHit hit;
				NavMesh.SamplePosition(end, out hit,1, 1);
				end=hit.position;
				traversingLink = true;
				return;
			}
			agent.transform.LookAt (new Vector3 (end.x, agent.transform.position.y, end.z));
			AnimatorStateInfo info= animator.GetCurrentAnimatorStateInfo(0);
			
			if(info.IsName(jumpState) && !animator.IsInTransition(0)){
				float tlerp =info.normalizedTime;
				var newPos = Vector3.Lerp(start, end, tlerp);
				newPos.y += 0.6f * Mathf.Sin(Mathf.PI * info.normalizedTime);
				agent.transform.position = newPos;

			}

			if (!info.IsName(jumpState) && !animator.IsInTransition(0) || Vector3.Distance(agent.transform.position,end)<0.3f)
			{
				traversingLink = false;
				agent.CompleteOffMeshLink();
				agent.Resume();
			}
			
		}
	}
	
	
}