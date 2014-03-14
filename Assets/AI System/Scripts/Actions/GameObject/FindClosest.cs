using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("GameObject")]
	[System.Serializable]
	public class FindClosest : BaseAction {
		[Tag()]
		public string tag="Untagged";
		[StoreParameter(true,typeof(GameObjectParameter))]
		public string store;

		public override void OnEnter ()
		{
			((GameObjectParameter)owner.GetParameter (store)).Value = Find ();
			//Debug.Log (((GameObjectParameter)owner.GetParameter (store)).Value== null);
			Finish ();
		}

		private GameObject Find(){
			GameObject[] tagged=GameObject.FindGameObjectsWithTag(tag);
			GameObject closest=null; 
			float distance = Mathf.Infinity; 
			Vector3 position = owner.transform.position; 
			foreach (GameObject go in tagged)  { 
				Vector3 diff = (go.transform.position - position);
				float curDistance = diff.sqrMagnitude; 
				if (curDistance < distance && go.transform != owner.transform) { 
					closest = go; 
					distance = curDistance; 
				} 
			} 
			return closest;
		}
	}
}