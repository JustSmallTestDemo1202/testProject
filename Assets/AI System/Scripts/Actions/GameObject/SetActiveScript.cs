using UnityEngine;
using System.Collections;


namespace AISystem.Actions{
	[Category("GameObject")]
	[System.Serializable]
	public class SetActiveScript : BaseAction {
		[StoreParameter(false,"Owner",typeof(GameObjectParameter))]
		public string target;
		public string script;
		public bool state;

		public override void OnEnter ()
		{
			GameObject ownerGo=(target =="Owner" || string.IsNullOrEmpty(target)?owner.gameObject:owner.GetGameObject(target));
			MonoBehaviour mComponent = ownerGo.GetComponent (script) as MonoBehaviour;
			if (mComponent != null) {
				mComponent.enabled=state;
			}
			Finish ();
		}
	}
}