// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Set the name of the Photon player.")]
	public class PhotonChatSetUserId : FsmStateAction
	{
		[Tooltip("The Photon chat user Id")]
		[RequiredField]
		public FsmString userId;
		
		public override void Reset()
		{
			userId = null;
		}
		
		public override void OnEnter()
		{
			if (userId == null)
			{
				return;
			}

			PlayMakerPhotonChatClient.AuthValues = new ExitGames.Client.Photon.Chat.AuthenticationValues(userId.Value);

			Finish();
		}
		
	}
}