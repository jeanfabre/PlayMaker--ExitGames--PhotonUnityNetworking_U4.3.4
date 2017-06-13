// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using ExitGames.Client.Photon.Chat;
using ExiGames.Client.Photon.Chat.Utils;

using HutongGames.PlayMaker.Actions;

public class PlayMakerPhotonChatClient : MonoBehaviour {

	public bool debug = false;
	
	// Use this for initialization
	void OnEnable () {
		ChatClientBroker.OnConnectedAction += OnConnected;
		ChatClientBroker.OnDisconnectedAction += OnDisconnected;
		ChatClientBroker.OnChatStateChangeAction += OnChatStateChange;
		ChatClientBroker.OnSubscribedAction += OnSubscribed;
	}

	void OnDisable() {
		ChatClientBroker.OnConnectedAction -= OnConnected;
		ChatClientBroker.OnDisconnectedAction -= OnDisconnected;
		ChatClientBroker.OnChatStateChangeAction -= OnChatStateChange;
		ChatClientBroker.OnSubscribedAction -= OnSubscribed;
	}


	#region Frame call Dispatching
	Queue<string> PendingActions = new Queue<string>();
	Queue<string[]> PendingLastChannels = new Queue<string[]>();
	Queue<bool[]> PendingLastResults = new Queue<bool[]>();
	Queue<ChatState> PendingLastStateChanges = new Queue<ChatState>();

	/// Because PlayMaker Fsm Execution will get shunted if severa similar messages are called within the same frame to reach the same Fsm, we implement a frame based dispatch.
	/// Only one distinct call per frame is allowed.
	void Update()
	{
	
		if (PendingActions.Count==0)
		{
			return;
		}

		string _action = PendingActions.Dequeue();

		switch (_action)
		{
			case "OnConnected":

			Debug.Log(Time.frameCount+" PHOTON / CHAT / ON CONNECTED");
			PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON CONNECTED");
			break;

			case "OnChatStateChange":
			
			Debug.Log(Time.frameCount+" PHOTON / CHAT / ON STATE CHANGE");
			
			PhotonChatGetOnChatStateChangeEventData.LastChatState = PendingLastStateChanges.Dequeue();
			PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON CHAT STATE CHANGE");

			break;

			case "OnSubscribed":

				PhotonChatGetOnSubscribeEventData.LastChannels = PendingLastChannels.Dequeue();
				PhotonChatGetOnSubscribeEventData.LastResults = PendingLastResults.Dequeue();
				
				Debug.Log(Time.frameCount+" PHOTON / CHAT / ON SUBSCRIBED: "+PhotonChatGetOnSubscribeEventData.LastChannels.ToStringFull());
				PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON SUBSCRIBED");

			break;

			default:
				Debug.LogError("unknown pending action <"+_action+">");
			break;
		}
	}

	#endregion

	
	#region PunChatClientBroker implementation
	

	public void DebugReturn (ExitGames.Client.Photon.DebugLevel level, string message)
	{
		//PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / DEBUG RETURN");
		//throw new System.NotImplementedException ();

		if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
		{
			UnityEngine.Debug.LogError(message);
		}
		else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
		{
			UnityEngine.Debug.LogWarning(message);
		}
		else
		{
			UnityEngine.Debug.Log(message);
		}

	}

	public void OnDisconnected ()
	{
		if(debug) Debug.Log("PlayMakerPhotonChatClient : OnDisconnected",this);
		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON DISCONNECTED");
	}

	public void OnConnected ()
	{
		if (debug) Debug.Log(Time.frameCount+" : PlayMakerPhotonChatClient : OnConnected",this);

		PendingActions.Enqueue("OnConnected");
	}

	public void OnChatStateChange (ChatState state)
	{
		if (debug) Debug.Log("PlayMakerPhotonChatClient : OnChatStateChange "+state,this);

		PendingLastStateChanges.Enqueue(state);
		PendingActions.Enqueue("OnChatStateChange");
	}

	public void OnGetMessages (string channelName, string[] senders, object[] messages)
	{
		if (debug) Debug.Log("PlayMakerPhotonChatClient : OnGetMessages for "+channelName,this);

		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON GET MESSAGES");
	}

	public void OnPrivateMessage (string sender, object message, string channelName)
	{
		if (debug) Debug.Log("PlayMakerPhotonChatClient : OnPrivateMessage from "+sender,this);

		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON PRIVATE MESSAGE");
	}


	public void OnSubscribed (string[] channels, bool[] results)
	{
		if (debug) Debug.Log(Time.frameCount+" : PlayMakerPhotonChatClient : OnSubscribed to "+channels.ToStringFull(),this);

		PendingLastChannels.Enqueue (channels);
		PendingLastResults.Enqueue(results);
		PendingActions.Enqueue("OnSubscribed");


	}

	public void OnUnsubscribed (string[] channels)
	{
		if (debug) Debug.Log("PlayMakerPhotonChatClient : OnUnsubscribed ",this);
		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON UNSUBSCRIBED");
	}

	public void OnStatusUpdate (string user, int status, bool gotMessage, object message)
	{
		if (debug) Debug.Log("PlayMakerPhotonChatClient : OnStatusUpdate for "+user+" status:"+status,this);
		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON STATUS UPDATE");
	}

	#endregion
}
