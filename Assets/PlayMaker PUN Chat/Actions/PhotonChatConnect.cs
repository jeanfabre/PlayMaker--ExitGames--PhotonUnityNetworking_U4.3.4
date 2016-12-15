// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

using ExitGames.Client.Photon.Chat;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Connect to Photon Chat")]
	public class PhotonChatConnect : FsmStateAction
	{
		[Tooltip("The Application Id")]
		public FsmString appId;

		[Tooltip("The gameVersion")]
		public FsmString gameVersion;

		[Tooltip("The userId")]
		public FsmString userId;

		[ActionSection("Result")]

		[Tooltip("The return of the connect call. True is ok, false means something went wrong")]
		[UIHint(UIHint.Variable)]
		public FsmBool result;

		[Tooltip("Tevent sent if connection call was initiated")]
		public FsmEvent successEvent;

		[Tooltip("Tevent sent if connection call failed")]
		public FsmEvent failureEvent;
		
		public override void Reset()
		{
			appId = null;
			gameVersion  = "1.0";
			userId = new FsmString(){UseVariable=true};
			result = null;
			successEvent = null;
			failureEvent = null;
		}
		
		public override void OnEnter()
		{

			if (!userId.IsNone)
			{
				PlayMakerPhotonChatClient.AuthValues = new ExitGames.Client.Photon.Chat.AuthenticationValues(userId.Value);
			}

			bool _result = PlayMakerPhotonChatClient.ChatClient.Connect(appId.Value, gameVersion.Value, PlayMakerPhotonChatClient.AuthValues);

			if (!result.IsNone)
			{
				result.Value = _result;
			}

			Fsm.Event(_result?successEvent:failureEvent);

			Finish();
		}
		

		
	}
}