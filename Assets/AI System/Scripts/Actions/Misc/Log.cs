using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("Misc")]
	[System.Serializable]
	public class Log : BaseAction {
		public string message;
		public override void OnEnter ()
		{
			Debug.Log (message);
		}
	}
}