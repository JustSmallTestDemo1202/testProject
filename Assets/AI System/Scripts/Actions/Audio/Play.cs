using UnityEngine;
using System.Collections;

namespace AISystem.Actions.Audio{
	[Category("Audio")]
	[System.Serializable]
	public class Play : BaseAction {
		[StoreParameter(false,"Owner",typeof(GameObjectParameter))]
		public string target;
		public AudioClip audioClip;
		public float volume;
		public float minDistance=3.0f;
		public float maxDistance=500.0f;


		public override void OnEnter ()
		{
			GameObject ownerGo=(target =="Owner" || string.IsNullOrEmpty(target)?owner.gameObject:((GameObjectParameter)owner.GetParameter (target)).Value);
			AudioSource audio=ownerGo.GetComponent<AudioSource>();
			if(audio == null){
				audio=ownerGo.AddComponent<AudioSource>();
			}
			audio.volume = volume;
			audio.minDistance = minDistance;
			audio.maxDistance = maxDistance;
			audio.clip=audioClip;
			audio.Play();
			Finish ();
		}
	}
}