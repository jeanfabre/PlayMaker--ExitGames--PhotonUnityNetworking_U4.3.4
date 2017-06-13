// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using ExiGames.Client.Photon.Chat.Utils;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Set the name of the Photon player.")]
	public class PhotonChatGetUserId : FsmStateAction
	{
		[Tooltip("The Photon chat user Id, will be empty if not found")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString userId;

		[Tooltip("True if userId found")]
		public FsmBool found;

		[Tooltip("Event Sent if the UserId is set")]
		public FsmEvent foundEvent;

		[Tooltip("Event Sent if the UserId is not set")]
		public FsmEvent missingEvent;
		
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

			if (ChatClientBroker.AuthValues!=null)
			{
				userId.Value = ChatClientBroker.AuthValues.UserId;
				found.Value = true;
				Fsm.Event(foundEvent);

			}else{
				userId.Value = string.Empty;
				found.Value = false;
				Fsm.Event(missingEvent);
			}

			Finish();
		}
		
	}
}