// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

using ExitGames.Client.Photon.Chat;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Check if Chat App Id is defined in the PhotonSettings")]
	public class PhotonChatAppIdCheck : FsmStateAction
	{

		[ActionSection("Result")]
		
		[Tooltip("True is if appid defined in settings")]
		[UIHint(UIHint.Variable)]
		public FsmBool result;
		
		[Tooltip("event sent if appid defined in settings")]
		public FsmEvent isSetEvent;
		
		[Tooltip("event sent if chat app id not defined in settings")]
		public FsmEvent isNotSetEvent;

		bool _result;
		public override void Reset()
		{

			result = null;
			isSetEvent = null;
			isNotSetEvent = null;
		}
		
		public override void OnEnter()
		{

			_result = !string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.ChatAppID);
			
			if (!result.IsNone)
			{
				result.Value = _result;
			}
			
			Fsm.Event(_result?isSetEvent:isNotSetEvent);
			
			Finish();
		}
		
		
		
	}
}