// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using System.Collections.Generic;

using ExitGames.Client.Photon.Chat;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Get the PHOTON / CHAT / ON SUBSCRIBE event data")]
	public class PhotonChatGetOnSubscribeEventData : FsmStateAction
	{

		/// <summary>
		/// The last state of channels list
		/// </summary>
		public static string[] LastChannels = new string[0];

		/// <summary>
		/// The last state of results list
		/// </summary>
		public static bool[] LastResults = new bool[0];


		[Tooltip("The number of channels subscribed to")]
		[UIHint(UIHint.Variable)]
		public FsmInt channelCount;

		[Tooltip("The number of channels successfully subscribed to")]
		[UIHint(UIHint.Variable)]
		public FsmInt validChannelCount;

		[Tooltip("The channels")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(FsmString))]
		public FsmArray validChannels;

		[Tooltip("The channels")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(FsmString))]
		public FsmArray channels;
		
		[Tooltip("The results")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(FsmBool))]
		public FsmArray results;

		List<string> _channelsOK;



		public override void Reset()
		{
			channelCount = null;
			validChannelCount = null;
			validChannels = null;
			channels = null;
			results = null;
		}
		
		public override void OnEnter()
		{
			_channelsOK = new List<string>();
			int i=0;
			foreach(string _channel in LastChannels)
			{
				if (LastResults[i])
				{
					_channelsOK.Add(_channel);
				}
				i++;
			}

			channelCount = LastChannels.Length;
			validChannelCount = _channelsOK.Count;

			if (!validChannels.IsNone)
			{
				validChannels.RawValue = _channelsOK.ToArray();
				validChannels.SaveChanges();
			}

			if (!channels.IsNone)
			{
				channels.stringValues = LastChannels;
				channels.SaveChanges();
			}
			if (!results.IsNone)
			{
				results.boolValues = LastResults;
				results.SaveChanges();
			}

			Finish();
		}
		
	}
}