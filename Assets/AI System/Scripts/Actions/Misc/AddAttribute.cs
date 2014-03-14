using UnityEngine;
using System.Collections;


namespace AISystem.Actions{
	[Category("Misc")]
	[System.Serializable]
	public class AddAttribute : BaseAction {
		[AttributePopup]
		public string attribute;
		public int amount;
		public override void OnEnter ()
		{
			BaseAttribute mAttribute= owner.GetAttribute (attribute);
			if (mAttribute != null) {
				mAttribute.Add(amount);
			}
			Finish ();
		}
	}
}