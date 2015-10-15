using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Check which platform we are running, useful to set various type of input corresponding to platform used use one action per platform")]
	public class CheckPlatform : FsmStateAction
	{
		public RuntimePlatform platform;
		public FsmEvent platformEvent;
		
		public override void Reset ()
		{
			platform = RuntimePlatform.OSXEditor;
			platformEvent = null;
		}
		
		public override void OnEnter ()
		{
			DoCheckPlatform();
			Finish();
		}
		
		void DoCheckPlatform()
		{
			if(Application.platform == platform)
				Fsm.Event(platformEvent);
		}
	}
}