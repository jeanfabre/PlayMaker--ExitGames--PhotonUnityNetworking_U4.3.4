// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon.Chat;

using HutongGames.PlayMaker.Actions;

public class PlayMakerPhotonChatClient : MonoBehaviour, IChatClientListener {
	
	public static ChatClient ChatClient;

	public static ExitGames.Client.Photon.Chat.AuthenticationValues AuthValues;

	public bool activeService = true;

	public bool debug = false;

	// Use this for initialization
	void Start () {
		ChatClient =  new ChatClient(this);
	}

	void Update() {

		if (PlayMakerPhotonChatClient.ChatClient != null && activeService)
		{
			PlayMakerPhotonChatClient.ChatClient.Service(); // make sure to call this regularly! it limits effort internally, so calling often is ok!
		}
	}

	#region IChatClientListener implementation

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
		if(debug) Debug.Log("OnDisconnected",this);
		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON DISCONNECTED");
	}

	public void OnConnected ()
	{
		if (debug) Debug.Log("OnConnected",this);
		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON CONNECTED");
	}

	public void OnChatStateChange (ChatState state)
	{
		if (debug) Debug.Log("OnChatStateChange "+state,this);

		PhotonChatGetChatStateChangeEventData.LastChatState = state;
		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON CHAT STATE CHANGE");
	}

	public void OnGetMessages (string channelName, string[] senders, object[] messages)
	{
		if (debug) Debug.Log("OnGetMessages for "+channelName,this);

		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON GET MESSAGES");
	}

	public void OnPrivateMessage (string sender, object message, string channelName)
	{
		if (debug) Debug.Log("OnPrivateMessage from "+sender,this);

		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON PRIVATE MESSAGE");
	}

	public void OnSubscribed (string[] channels, bool[] results)
	{
		if (debug) Debug.Log("OnSubscribed",this);

		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON SUBSCRIBED");
	}

	public void OnUnsubscribed (string[] channels)
	{
		if (debug) Debug.Log("OnUnsubscribed",this);
		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON UNSUBSCRIBED");
	}

	public void OnStatusUpdate (string user, int status, bool gotMessage, object message)
	{
		if (debug) Debug.Log("OnStatusUpdate for "+user+" status:"+status,this);
		PlayMakerFSM.BroadcastEvent ("PHOTON / CHAT / ON STATUS UPDATE");
	}

	#endregion
}
