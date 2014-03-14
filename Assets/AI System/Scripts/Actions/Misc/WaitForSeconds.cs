using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("Misc")]
	[System.Serializable]
	public class WaitForSeconds : BaseAction {
		[MinMax(0,20)]
		public MinMax seconds;

		private float time;
		public override void OnEnter ()
		{
			time = Time.time + seconds.GetRandom ();
		}

		public override void OnUpdate ()
		{
			if (Time.time > time) {
				Finish();
			}
		}
	}
}