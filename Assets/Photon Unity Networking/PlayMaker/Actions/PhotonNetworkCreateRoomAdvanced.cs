// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using System.Collections;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon")]
	[Tooltip("Create a room With advanced settings.Please refer to Photon documentation.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1134")]
	public class PhotonNetworkCreateRoomAdvanced : FsmStateAction
	{
		[Tooltip("The room Name")]
		public FsmString roomName;
		
		[Tooltip("Is the room visible")]
		public FsmBool isVisible;
		
		[Tooltip("Is the room open")]
		public FsmBool isOpen;
			
		[Tooltip("Max numbers of players for this room.")]
		public FsmInt maxNumberOfPLayers;
		
		[ActionSection("Custom Properties")]
		
		[Tooltip("Clean up on room leaving. Leave to 'none' to use the default value")]
		public FsmBool cleanupCacheOnLeave;
			
		
		[CompoundArray("Count", "Key", "Value")]
		[Tooltip("The Custom Property to set")]
		public FsmString[] customPropertyKey;
		[RequiredField]
		[Tooltip("Value of the property")]
		public FsmVar[] customPropertyValue;
		
		[ActionSection("Lobby custom Properties")]
		[Tooltip("Properties listed in the lobby.")]
		public FsmString[] lobbyCustomProperties;
		
		public override void Reset()
		{
			roomName  = new FsmString() {UseVariable=true};
			isVisible = true;
			isOpen = true;
			
			cleanupCacheOnLeave = new FsmBool() {UseVariable=true};
			maxNumberOfPLayers = 100;
			customPropertyKey = null;
			customPropertyValue = null;
			lobbyCustomProperties = null;
		}

		public override void OnEnter()
		{
			
		
			string _roomName = null;
			if ( ! string.IsNullOrEmpty(roomName.Value) )
			{
				_roomName = roomName.Value;
			}
				

			ExitGames.Client.Photon.Hashtable _props = new ExitGames.Client.Photon.Hashtable();
			
			int i = 0;
			foreach(FsmString _prop in customPropertyKey)
			{
				_props[_prop.Value] =  PlayMakerPhotonProxy.GetValueFromFsmVar(this.Fsm,customPropertyValue[i]);
				i++;
			}
			
			
			string[] lobbyProps = new string[lobbyCustomProperties.Length];
			
			int j = 0;
			foreach(FsmString _visibleProp in lobbyCustomProperties)
			{
				lobbyProps[j] = _visibleProp.Value;
				j++;
			}

			RoomOptions _options = new RoomOptions();
			_options.maxPlayers =  (byte)maxNumberOfPLayers.Value;
			_options.isVisible = isVisible.Value;
			_options.isOpen = isOpen.Value;
			_options.customRoomProperties = _props;
			_options.customRoomPropertiesForLobby = lobbyProps;
			if (!cleanupCacheOnLeave.IsNone)
			{
			_options.cleanupCacheOnLeave = cleanupCacheOnLeave.Value;
			}

			PhotonNetwork.CreateRoom(_roomName,_options,TypedLobby.Default);
			
			
			Finish();
		}

	}
}