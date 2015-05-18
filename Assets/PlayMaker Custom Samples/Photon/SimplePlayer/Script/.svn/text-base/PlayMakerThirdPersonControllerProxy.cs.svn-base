// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;
using System.Collections;


/// <summary>
/// Play maker third person controller proxy.
/// This proxy simply allow playmaker top access the CharacterState enum of a ThirdPersonController. This is necessary until PLaymaker supports enums access.
/// This proxy also prevents having to modify the ThirdPersonController script. So that we can provide a better port and show this kind of options when
/// users wonders about integrating existing scripts and find themselves into similar situations.
/// </summary>
[RequireComponent (typeof (ThirdPersonController))]
public class PlayMakerThirdPersonControllerProxy : MonoBehaviour {

	
	/// <summary>
	/// Gets or sets the index of the character state. Use "set property" and "get property" within playmaker to access this variable.
	/// </summary>
	/// <value>
	/// The index of the character state
	/// </value>
	public int characterStateIndex
	{
		
		get{
			if(_controller!=null)
			{
				return (int)_controller._characterState;
			}
			return 0;
		}
		
		set{
			if(_controller!=null)
			{
				_controller._characterState =  (CharacterState)value;
			}
		}
	}
	
	/// <summary>
	/// The ThirdPersonController component pointer.
	/// </summary>
	private ThirdPersonController _controller;
	
	
	/// <summary>
	/// Get the ThirdPersonController pointer.
	/// </summary>
	void Awake()
	{
		 _controller = GetComponent<ThirdPersonController>();
		if (_controller== null)
		{
			Debug.Log("ThirdPersonController component could not be found.");
		}
	}
}
