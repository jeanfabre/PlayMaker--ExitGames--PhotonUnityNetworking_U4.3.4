// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.CharacterController)]
	[Tooltip("Gets the state of a character controller using ints. Useful for network games where performances is needed")]
	public class CharacterControllerGetState : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(ThirdPersonController))]
        [Tooltip("The GameObject with the character controller.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
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
			
			SetState();
			
			if (!everyFrame)
			{
				Finish();	
			}
		}

		public override void OnUpdate()
		{
			SetState();
		}
		
		private void SetState()
		{
			if (_controller != null)
			{
				state.Value = (int)_controller._characterState;
			}
		}
	}
}