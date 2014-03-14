using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("Parameter")]
	[System.Serializable]
	public class SetRandomInt : BaseAction {
		[StoreParameter(true,typeof(IntParameter))]
		public string store;
		[MinMax(-20,20f,true)]
		public MinMax value;
		
		public override void OnEnter ()
		{
			int val = Mathf.RoundToInt (value.GetRandom ());
			owner.SetInt(store,val);
			Finish ();
		}
	}
}