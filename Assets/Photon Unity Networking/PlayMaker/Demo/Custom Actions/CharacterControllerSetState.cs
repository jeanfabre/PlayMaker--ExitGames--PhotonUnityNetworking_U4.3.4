// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.CharacterController)]
	[Tooltip("Sets the state of a character controller using ints. Useful for network games where performances is needed")]
	public class CharacterControllerSetState : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(ThirdPersonController))]
        [Tooltip("The GameObject with the character controller.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		public FsmInt state;
		
		public bool everyFrame;
		
		private ThirdPersonController _controller;
		
		public override void Reset()
		{
			gameObject = null;
			
			state = null;
			
			everyFrame = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			_controller = go.GetComponent<ThirdPersonController>();

			if (_controller == null)
			{
				return;
			}
			
			GetState();
			
			if (!everyFrame)
			{
				Finish();	
			}
		}

		public override void OnUpdate()
		{
			GetState();
		}
		
		private void GetState()
		{
			if (_controller != null)
			{
				_controller._characterState =  (CharacterState)state.Value;
			}
		}
	}
}