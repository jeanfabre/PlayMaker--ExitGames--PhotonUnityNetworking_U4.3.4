// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using ExitGames.Client.Photon.Chat;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Get the PHOTON / CHAT / ON CHAT STATE CHANGE event data")]
	public class PhotonChatGetChatStateChangeEventData : FsmStateAction
	{

		/// <summary>
		/// The last state of the chat.
		/// </summary>
		public static ChatState LastChatState;


		[Tooltip("The chat state")]
		[RequiredField]
		[ObjectType(typeof(ChatState))]
		public FsmEnum chatState;
		

		public override void Reset()
		{
			chatState.RawValue = 0;
		}
		
		public override void OnEnter()
		{
			chatState.Value = LastChatState;

			Finish();
		}
		
	}
}