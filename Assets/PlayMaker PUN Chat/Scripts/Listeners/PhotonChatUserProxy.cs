using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker.Ecosystem.Utils;

using ExiGames.Client.Photon.Chat.Utils;

public class PhotonChatUserProxy : MonoBehaviour, IPunChatUser {

	string _userId;

	public string UserId
	{
		get{
			return _userId;
		}
		set{

			ChatClientBroker.Unregister(this);
			
			_userId = value;

			ChatClientBroker.Register(this);
		}
	}

	
	public PlayMakerEventTarget eventTarget = new PlayMakerEventTarget(true);
	
	[EventTargetVariable("eventTarget")]
	public PlayMakerEvent OnStatusUpdateEvent= new PlayMakerEvent("PHOTON / CHAT / USER / ON STATUS UPDATE");

	public bool debug = false;


	#region MonoBehavior Callbacks

	void OnEnable()
	{
		ChatClientBroker.Register(this);
	}

	void OnDisable()
	{
		ChatClientBroker.Unregister(this);
	}


	#endregion

	#region IPunChatUser implementation

	void IPunChatUser.OnStatusUpdate (int status, bool gotMessage, object message)
	{
		if (debug)
		{
			UnityEngine.Debug.Log("OnStatusUpdate "+status+" "+gotMessage+" for user "+UserId+" on GameObject: "+this.gameObject.name);
		}
		PhotonChatUserGetOnStatusUpdateEventData.LastStatus = status;
		PhotonChatUserGetOnStatusUpdateEventData.LastGotMessage = gotMessage;
		PhotonChatUserGetOnStatusUpdateEventData.LastMessage = message;

	//	GetLastPointerDataInfo.lastPointeEventData = data;
		OnStatusUpdateEvent.SendEvent(null,eventTarget);
	}

	void IPunChatUser.OnGetMessages (string channel, string[] senders, object[] messages)
	{
		//throw new System.NotImplementedException ();
	}

	void IPunChatUser.OnPrivateMessage (string sender, object message, string channelName)
	{
		//throw new System.NotImplementedException ();
	}

	string IPunChatUser.User {
		get {
			return UserId;
		}
		set {
			UserId = value;
		}
	}

	#endregion
}
