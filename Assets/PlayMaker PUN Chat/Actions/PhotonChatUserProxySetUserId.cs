// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Photon Chat")]
	[Tooltip("Set the userId of a Photon player User Proxy.")]
	public class PhotonChatUserProxySetUserId : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(PhotonChatUserProxy))]
		[Tooltip("The GameObject with the PhotonChatUserProxy component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Photon chat user Id")]
		[RequiredField]
		public FsmString userId;

		PhotonChatUserProxy _proxy;
		public override void Reset()
		{
			gameObject = null;
			userId = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				LogError("Missing GameObject target");
				return;
			}
			
			_proxy = go.GetComponent<PhotonChatUserProxy>();
			if (_proxy == null)
			{
				LogError("Missing PhotonChatUserProxy component target");
				return;
			}

			_proxy.UserId = userId.Value;

			Finish();
		}
		
	}
}