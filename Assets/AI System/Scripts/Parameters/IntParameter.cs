using UnityEngine;
using System.Collections;

namespace AISystem{
	[System.Serializable]
	public class IntParameter : NamedParameter {
		[SerializeField]
		private int value;
		
		public int Value
		{
			get{
				return this.value;
			}
			set{
				this.value = value;
			}
		}
	}
}