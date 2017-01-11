// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon")]
	[Tooltip("Store the list of Photon players in an ArrayList")]
	public class ArrayMakerGetPhotonPlayersInRoom : CollectionsActions
	{
		
		[ActionSection("Array referencing")]
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The ArrayList reference that will host the players names")]
		public FsmString NameReference;
		
		[Tooltip("The ArrayList reference that will host the players id")]
		public FsmString IdReference;
		
		[ActionSection("Options")]
		[Tooltip("If true, list only other players.")]
		public FsmBool otherPLayerOnly;
		
	
		private PhotonPlayer[] players;
		private string[] playerNames;
		
		
		
		public override void Reset()
		{
			gameObject = null;
			otherPLayerOnly = true;
			NameReference = "Photon Player Name";
			IdReference = "Photon Player ID";
		}
		
		public override void OnEnter()
		{
			if (otherPLayerOnly.Value){
				players = PhotonNetwork.otherPlayers;
			}else{
				players = PhotonNetwork.playerList;
			}
							//
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			// -------- Player -----------//
			// get each arrayList reference
			PlayMakerArrayListProxy nameList = GetArrayListProxyPointer(go,NameReference.Value,false);
			
			/*
			if (nameList ==null)
			{
				nameList = go.AddComponent<PlayMakerArrayListProxy>();
				nameList.referenceName = "Photon Player Name";
			}
			*/
			
			PlayMakerArrayListProxy idList = GetArrayListProxyPointer(go,IdReference.Value,false);
			/*
			if (idList ==null)
			{
				idList = go.AddComponent<PlayMakerArrayListProxy>();
				idList.referenceName = "Photon Player ID";
			}
			*/
			
			nameList.arrayList.Clear();
			idList.arrayList.Clear();
			
			foreach (PhotonPlayer player in players)
            {
				nameList.arrayList.Add(player.NickName);
				idList.arrayList.Add(player.ID);
			}
	
		}
	}
}
