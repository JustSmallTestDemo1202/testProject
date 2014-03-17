using UnityEngine;
using System.Collections;

namespace AISystem.Actions.NavMeshAgent{
	[Category("NavMeshAgent")]
	[System.Serializable]
	public class GetProjectedVelocity : BaseAction {
		[StoreParameter(true,typeof(FloatParameter))]
		public string store;
		private UnityEngine.NavMeshAgent agent;

		public override void OnAwake ()
		{
			agent = owner.GetComponent<UnityEngine.NavMeshAgent> ();
		}

		public override void OnUpdate ()
		{
			((FloatParameter)owner.GetParameter(store)).Value=Vector3.Project(agent.desiredVelocity, owner.transform.forward).magnitude;
		}
	}
}