using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("GameObject")]
	[System.Serializable]
	public class Instantiate : BaseAction {
		public GameObject prefab;
		[UseGlobalPrameter("globalPosition",typeof(GameObjectParameter))]
		public bool useGlobal;
		[Hide]
		public string globalPosition;
		[Hide("useGlobal")]
		public Vector3 position;
		public Vector3 angle;
		[StoreParameter(false,typeof(GameObjectParameter))]
		public string store;

		public override void OnEnter ()
		{
			position = useGlobal ? owner.GetGameObject(globalPosition).transform.position : position;
			GameObject go=(GameObject)GameObject.Instantiate(prefab,position,Quaternion.Euler(angle));
			if (store != "None") {
				((GameObjectParameter)owner.GetParameter (store)).Value = go;
			}
			Finish ();
		}
	}
}