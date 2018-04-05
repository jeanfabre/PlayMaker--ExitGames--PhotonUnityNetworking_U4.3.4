// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.

using System;
using UnityEngine;
using ExitGames.Client.Photon.Chat;
using System.Linq;

using ChatUserStatus = PlayMaker.PUN.Chat.ChatUserStatus;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Get the PHOTON / CHAT / USER / ON SENT MESSAGE event data. This event will be fired by PhotonChatUserProxy.")]
	public class PhotonChatUserGetOnSentMessageEventData : FsmStateAction
	{
		/// <summary>
		/// The last "User" sending a message.
		/// </summary>
		public static string LastUser;


		/// <summary>
		/// The last "Senders" sending a message.
		/// </summary>
		public static string[] LastSenders;

		/// <summary>
		/// The last "Channel" this user sent a message to.
		/// </summary>
		public static string LastChannel;

		/// <summary>
		/// The last messages of this user.
		/// </summary>
		public static object[] LastMessages;


		[Tooltip("The user")]
		[UIHint(UIHint.Variable)]
		public FsmString user;

		[Tooltip("The senders. Should be the same as user")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.String)]
		public FsmArray  senders;


		[Tooltip("The channel")]
		[UIHint(UIHint.Variable)]
		public FsmString channel;

		[Tooltip("The number of messages.")]
		[UIHint(UIHint.Variable)]
		public FsmInt messagesCount;

		[Tooltip("The messages")]
		[UIHint(UIHint.Variable)]
		public FsmVar[] messages;

		[Tooltip("The messages as string")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.String)]
		public FsmArray messagesAsString;

		public override void Reset()
		{
			user = null;
			senders = null;
			channel = null;
			messages = null;
			messagesAsString = null;
		}
		
		public override void OnEnter()
		{
			user.Value = LastUser;
			channel.Value = LastChannel;
			messagesCount.Value = LastMessages.Length;

			if (!senders.IsNone) {
				senders.Values = LastSenders;
			}

			if (!messagesAsString.IsNone) {
				messagesAsString.Values = LastMessages.Cast<object> ()
					.Select (x => x.ToString ())
					.ToArray ();

			}

			int i = 0;
			foreach (FsmVar _var in messages) {
				if (LastMessages.Length>i)
				{
					PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_var,LastMessages[i]);
				}
				i++;
			}

			Finish();
		}
		
	}
}