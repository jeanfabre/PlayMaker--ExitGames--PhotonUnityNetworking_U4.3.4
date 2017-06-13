using System;
using System.Collections;

using UnityEditor;
using UnityEngine;

namespace  HutongGames.PlayMaker.Ecosystem.utils
{
	[CustomEditor(typeof(PhotonChatUserProxy))]
	public class PhotonChatUserProxyInspector : UnityEditor.Editor {
		
		public override void OnInspectorGUI()
		{
			
			PhotonChatUserProxy _target = (PhotonChatUserProxy)this.target;

			DrawDefaultInspector();


			if (Application.isPlaying)
			{
				string _userdId = string.IsNullOrEmpty(_target.UserId)?"n/a":_target.UserId;
			
				EditorGUILayout.LabelField("UserId",_userdId);
			}

		}
	}
}