using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("GameObject")]
	[System.Serializable]
	public class FindGameObject : BaseAction {
		[Tag()]
		public string tag="Untagged";
		[StoreParameter(true,typeof(GameObjectParameter))]
		public string store;

		public override void OnEnter ()
		{
			((GameObjectParameter)owner.GetParameter (store)).Value = GameObject.FindGameObjectWithTag (tag);
			Finish ();
		}
	}
}