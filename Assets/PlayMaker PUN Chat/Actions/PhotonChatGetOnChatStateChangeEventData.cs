// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using ExitGames.Client.Photon.Chat;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Get the PHOTON / CHAT / ON CHAT STATE CHANGE event data")]
	public class PhotonChatGetOnChatStateChangeEventData : FsmStateAction
	{

		/// <summary>
		/// The last state of the chat.
		/// </summary>
		public static ChatState LastChatState;

		[Tooltip("The chat state")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(ChatState))]
		public FsmEnum chatState;

		[Tooltip("The chat state  as string")]
		[UIHint(UIHint.Variable)]
		public FsmString chatStateAsString;


		public override void Reset()
		{
			chatState = null ;//.RawValue = 0;
			chatStateAsString = null;
		}
		
		public override void OnEnter()
		{
			if (!chatState.IsNone) chatState.Value = LastChatState;
			if (!chatStateAsString.IsNone) chatStateAsString.Value = LastChatState.ToString();

			Finish();
		}
		
	}
}