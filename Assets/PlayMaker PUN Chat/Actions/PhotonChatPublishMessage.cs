// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
	
using UnityEngine;
using ExiGames.Client.Photon.Chat.Utils;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Sends a message to a public channel which this client subscribed to.")]
	public class PhotonChatPublishMessage : FsmStateAction
	{
		[Tooltip("The channel name to publish message")]
		[RequiredField]
		public FsmString channel;

		[Tooltip("The channel name to publish message")]
		[RequiredField]
		public FsmString message;

		[ActionSection("Result")]
		
		[Tooltip("The return of the publication call. True is ok, false means something went wrong")]
		[UIHint(UIHint.Variable)]
		public FsmBool result;
		
		[Tooltip("Event sent if connection call was initiated")]
		public FsmEvent successEvent;
		
		[Tooltip("Event sent if connection call failed")]
		public FsmEvent failureEvent;

		public override void Reset()
		{
			channel = null;
			message = null;
			result = null;
			successEvent = null;
			failureEvent = null;
		}
		
		public override void OnEnter()
		{
			bool _result = ChatClientBroker.ChatClient.PublishMessage(channel.Value, message.Value);

			if (!result.IsNone)
			{
				result.Value = _result;
			}
			
			Fsm.Event(_result?successEvent:failureEvent);

			Finish();
		}
		
	}
}