// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the text value of a UGui Text component using an Int value.")]
	public class uGuiTextSetTextUsingInt : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Text))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The value to inject in the text.")]
		public FsmInt textValue;

		[UIHint(UIHint.TextArea)]
		[Tooltip("The text pattern to use to wrap the textvalue. {0}")]
		public FsmString textFormat;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Text _text;
		string _originalString;

		public override void Reset()
		{
			gameObject = null;
			textValue = null;
			textFormat = new FsmString() {UseVariable= true};
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_text = _go.GetComponent<UnityEngine.UI.Text>();
			}

			if (resetOnExit.Value)
			{
				_originalString = _text.text;
			}

			DoSetTextValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetTextValue();
		}
		
		void DoSetTextValue()
		{

			if (_text!=null)
			{
				_text.text = textFormat.IsNone?textValue.Value.ToString():string.Format(textFormat.Value,textValue.Value);
			}
		}

		public override void OnExit()
		{
			if (_text==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_text.text = _originalString;
			}
		}
	}
}