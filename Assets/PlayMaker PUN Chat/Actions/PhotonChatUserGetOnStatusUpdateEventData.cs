// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.

using System;
using UnityEngine;
using ExitGames.Client.Photon.Chat;

using ChatUserStatus = PlayMaker.PUN.Chat.ChatUserStatus;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Get the PHOTON / CHAT / USER / ON STATUS UPDATE event data. This event will be fired by PhotonChatUserProxy. It allows you to select other events for the status Update")]
	public class PhotonChatUserGetOnStatusUpdateEventData : FsmStateAction
	{

		/// <summary>
		/// The last status of this user.
		/// </summary>
		public static int LastStatus;

		/// <summary>
		/// The last "got message" of this user.
		/// </summary>
		public static bool LastGotMessage;

		/// <summary>
		/// The last message of this user.
		/// </summary>
		public static object LastMessage;

		[Tooltip("The user status")]
		[UIHint(UIHint.Variable)]
		public FsmEnum status;

		[Tooltip("The user status  as int")]
		[UIHint(UIHint.Variable)]
		public FsmInt statusAsInt;

		[Tooltip("The user 'Got Message' property")]
		[UIHint(UIHint.Variable)]
		public FsmBool gotMessage;

		[Tooltip("The user message property")]
		[UIHint(UIHint.Variable)]
		public FsmVar message;

		[Tooltip("Event Sent is if User Got Message")]
		public FsmEvent gotMessageEvent;


		public override void Reset()
		{
			status = new FsmEnum() {ObjectType=typeof(ChatUserStatus)};
			statusAsInt = null;

			gotMessage = null;

			message = null;

			gotMessageEvent = null; 
		}
		
		public override void OnEnter()
		{
			if (!status.IsNone) status.Value = (Enum) Enum.ToObject(status.EnumType, LastStatus);
			if (!statusAsInt.IsNone) statusAsInt.Value = LastStatus;

			if (!gotMessage.IsNone) gotMessage.Value = LastGotMessage;

			if (!message.IsNone){
				if (message.Type == VariableType.Array)
				{
					object[] _lastMessages = (object[]) LastMessage;
					if (_lastMessages!=null)
					{
						PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,message,_lastMessages);
					}
				}else{
					message.SetValue(LastMessage);
				}
			}

			if (gotMessageEvent!=null && LastGotMessage) this.Fsm.Event(gotMessageEvent);

			Finish();
		}
		
	}
}