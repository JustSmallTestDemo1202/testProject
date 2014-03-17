using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("GameObject")]
	[System.Serializable]
	public class Destroy : BaseAction {
		[StoreParameter(false,"Owner",typeof(GameObjectParameter))]
		public string target;
		public float delay;

		public override void OnEnter ()
		{
			GameObject ownerGo=(target =="Owner" || string.IsNullOrEmpty(target)?owner.gameObject:((GameObjectParameter)owner.GetParameter (target)).Value);
			GameObject.Destroy (ownerGo, delay);
			Finish ();
		}
	}
}