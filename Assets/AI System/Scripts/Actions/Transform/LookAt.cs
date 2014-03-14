using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("Transform")]
	[System.Serializable]
	public class LookAt : BaseAction {
		[StoreParameter(false,"Owner",typeof(GameObjectParameter))]
		public string who;
		[StoreParameter(true,typeof(GameObjectParameter))]
		public string target;
		public Vector3 offset;
		public Vector3 ignore=Vector3.up;

		private GameObject mTarget;
		private GameObject ownerGo;
		public override void OnEnter ()
		{
			if (mTarget == null) {
				mTarget = ((GameObjectParameter)owner.GetParameter (target)).Value;
			}
			if (ownerGo == null) {
				ownerGo = (who == "Owner" || string.IsNullOrEmpty (who) ? owner.gameObject : ((GameObjectParameter)owner.GetParameter (who)).Value);
			}
		}


		public override void OnUpdate ()
		{
			if (mTarget != null && ownerGo != null) {
				Vector3 position = mTarget.transform.position;
				Vector3 ownerPosition=ownerGo.transform.position;

				position.x=(ignore.x>0?ownerPosition.x:position.x);
				position.y=(ignore.y>0?ownerPosition.y:position.y);
				position.z=(ignore.z>0?ownerPosition.z:position.z);
				ownerGo.transform.LookAt ((position+offset));
			}
			Finish ();
		}
	}
}