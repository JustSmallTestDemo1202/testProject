using UnityEngine;
using System.Collections;


namespace AISystem.Actions{
	[Category("Transform")]
	[System.Serializable]
	public class Translate : BaseAction {
		[StoreParameter(false,"Owner",typeof(GameObjectParameter))]
		public string who;
		public Vector3 direction=Vector3.forward;

		public override void OnUpdate ()
		{
			GameObject ownerGo=(who =="Owner" || string.IsNullOrEmpty(who)?owner.gameObject:((GameObjectParameter)owner.GetParameter (who)).Value);
			if (ownerGo != null) {
				ownerGo.transform.Translate (direction * Time.deltaTime);		
			}
			Finish ();
		}
	}
}