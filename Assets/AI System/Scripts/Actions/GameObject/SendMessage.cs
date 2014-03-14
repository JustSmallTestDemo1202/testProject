using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("GameObject")]
	[System.Serializable]
	public class SendMessage : BaseAction {
		[StoreParameter(false,"Owner",typeof(GameObjectParameter))]
		public string target;
		public string message;
		public MessageParameterType parameter;
		public int intParameter;
		public float floatParameter;
		public string stringParameter;
		public Object objectParameter;

		public override void OnEnter ()
		{
			GameObject ownerGo=(target =="Owner" || string.IsNullOrEmpty(target)?owner.gameObject:((GameObjectParameter)owner.GetParameter (target)).Value);
			switch (parameter) {
			case MessageParameterType.Float:
				ownerGo.SendMessage(message,floatParameter);
				break;
			case MessageParameterType.Int:
				ownerGo.SendMessage(message,intParameter);
				break;
			case MessageParameterType.String:
				ownerGo.SendMessage(message,stringParameter);
				break;
			case MessageParameterType.Object:
				ownerGo.SendMessage(message,objectParameter);
				break;
			default:
				ownerGo.SendMessage(message);
				break;
			}
			Finish ();
		}

		public enum MessageParameterType{
			None,
			Int,
			Float,
			String,
			Object
		}
	}
}