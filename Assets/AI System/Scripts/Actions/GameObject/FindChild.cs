using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("GameObject")]
	[System.Serializable]
	public class FindChild : BaseAction {
		public string childName;
		[StoreParameter(true,typeof(GameObjectParameter))]
		public string store;

		public override void OnEnter ()
		{
			Transform child = Find (owner.transform);
			if (child != null) {
				((GameObjectParameter)owner.GetParameter (store)).Value = child.gameObject;
			}
			Finish ();
		}

		private Transform Find(Transform target)
		{
			if (target.name == childName) 
				return target;
			
			for (int i = 0; i < target.transform.childCount; ++i)
			{
				Transform result = Find(target.GetChild(i));
				
				if (result != null) 
					return result;
			}
			return null;
		}
	}
}