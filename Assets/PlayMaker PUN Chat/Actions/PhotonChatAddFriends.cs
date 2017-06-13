// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.

using UnityEngine;
using ExiGames.Client.Photon.Chat.Utils;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Adds friends to a list on the Chat Server which will send you status updates for those.")]
	public class PhotonChatAddFriends : FsmStateAction
	{
		[Tooltip("The list of friends' userids")]
		[RequiredField]
		[ArrayEditor(VariableType.String)]
		public FsmArray friends;

		[Tooltip("true if call was made, false if the operation could not be sent ")]
		[UIHint(UIHint.Variable)]
		public FsmBool success;

		[Tooltip("Sent if the operation could be sent ")]
		public FsmEvent successEvent;

		[Tooltip("Sent if the operation could not be sent ")]
		public FsmEvent failureEvent;

		public override void Reset()
		{
			friends = null;
		}
		
		public override void OnEnter()
		{
			bool _result = ChatClientBroker.ChatClient.AddFriends(friends.stringValues);

		 	if (!success.IsNone) success.Value = _result;

			Fsm.Event(_result?successEvent:failureEvent);


			Finish();
		}
	}
}