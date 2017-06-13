// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

using ExitGames.Client.Photon.Chat;
using ExiGames.Client.Photon.Chat.Utils;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Connect to Photon Chat")]
	public class PhotonChatConnect : FsmStateAction
	{
		[Tooltip("The Application Id. Leave to none to use the one from the Settings")]
		public FsmString appId;

		[Tooltip("The gameVersion")]
		public FsmString gameVersion;

		[Tooltip("The userId")]
		public FsmString userId;

		[ActionSection("Result")]

		[Tooltip("The return of the connect call. True is ok, false means something went wrong")]
		[UIHint(UIHint.Variable)]
		public FsmBool result;

		[Tooltip("Event sent if connection call was initiated")]
		public FsmEvent successEvent;

		[Tooltip("Event sent if connection call failed")]
		public FsmEvent failureEvent;
		
		public override void Reset()
		{
			appId = new FsmString(){UseVariable = true};
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
				ChatClientBroker.AuthValues = new ExitGames.Client.Photon.Chat.AuthenticationValues(userId.Value);
			}

			string _appId = PhotonNetwork.PhotonServerSettings.ChatAppID;

			if (!appId.IsNone)
			{
				_appId = appId.Value;
			}


			bool _result = ChatClientBroker.ChatClient.Connect(_appId, gameVersion.Value, ChatClientBroker.AuthValues);

			if (!result.IsNone)
			{
				result.Value = _result;
			}

			Fsm.Event(_result?successEvent:failureEvent);

			Finish();
		}
		

		
	}
}