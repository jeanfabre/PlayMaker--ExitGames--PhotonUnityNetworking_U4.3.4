// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using PlayMaker.PUN.Chat;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Set the user's online status. You can define your own.")]

	public class PhotonChatSetOnlineStatus : FsmStateAction
	{
		[Tooltip("The online status")]
		public FsmEnum onlineStatus;

		public override void Reset()
		{
			onlineStatus = new FsmEnum(){EnumType=typeof(ChatUserStatus)};
		}
		
		public override void OnEnter()
		{
			PlayMakerPhotonChatClient.ChatClient.SetOnlineStatus((int)onlineStatus.RawValue);

			Finish();
		}
		
	}
}