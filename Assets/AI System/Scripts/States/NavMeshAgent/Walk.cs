using UnityEngine;
using System.Collections;

namespace AISystem.States.NavMeshAgent{
	[Category("NavMeshAgent")]
	[System.Serializable]
	public class Walk : Simple {
		public float speed=2.0f;
		public float rotation=150.0f;
		public float range=10.0f;
		public float threshold=0.1f;		

		private Vector3 initialPosition;
		//Called once, use for initialization
		public override void OnAwake ()
		{
			base.OnAwake ();
			initialPosition = owner.transform.position;
		}

		// Same as Update()
		public override void OnUpdate ()
		{
			if (agent.remainingDistance < threshold) {
				Vector3 destination=GetRandomDestination(true);
				NavMeshHit hit;
				NavMesh.SamplePosition(destination, out hit, range, 1);
				destination = hit.position;
				agent.SetDestination(destination);
			}
		}

		//Called when the state is entered.
		public override void OnEnter ()
		{
			agent.speed = speed;
			agent.angularSpeed = rotation;
		}

		//Called when the state is finished and there is a new transition to another state
		public override void OnExit ()
		{
			agent.Stop ();
			agent.SetDestination(owner.transform.position);
		}
		
		private Vector3 GetRandomDestination(bool raycast){
			Vector3 random = new Vector3 (initialPosition.x + Random.Range (-range, range), initialPosition.y, initialPosition.z + Random.Range (-range, range)); 
			if (raycast) {
				RaycastHit hit;
				if (Physics.Raycast (random + Vector3.up * 500, Vector3.down, out hit)) {
					random.y = hit.point.y;
				}
			}
			return random;
		}
	}
}