// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Subscribe to an array of channels.")]
	public class PhotonChatSubscribeToChannelsArray : FsmStateAction
	{
		[Tooltip("The channels to subscribe")]
		[RequiredField]
		[ArrayEditor(VariableType.String)]
		public FsmArray channels;

		[ActionSection("Result")]
		
		[Tooltip("The return of the subscription call. True is ok, false means something went wrong")]
		[UIHint(UIHint.Variable)]
		public FsmBool result;
		
		[Tooltip("Event sent if subscription call was initiated")]
		public FsmEvent successEvent;
		
		[Tooltip("Event sent if connection call failed")]
		public FsmEvent failureEvent;

		public override void Reset()
		{
			channels = null;
		}
		
		public override void OnEnter()
		{
			bool _ok = PlayMakerPhotonChatClient.ChatClient.Subscribe(channels.stringValues);

			if (!result.IsNone) result.Value = _ok;

			Fsm.Event(_ok?successEvent:failureEvent);

			Finish();
		}
		
	}
}