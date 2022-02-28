using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteractable : Interactable
{
	//Apply this script to every object you want to interact with
	public override void OnFocus()
	{
		//Debug.Log("Looking At: " + gameObject.name);
	}

	public override void OnInteract()
	{
		//Debug.Log("Interacting With: " + gameObject.name);
	}

	public override void OnLoseFocus()
	{
		//Debug.Log("Lost Focus From: " + gameObject.name);
	}
	#region Variables
	#endregion

	#region Singleton
	#endregion

	#region Unity Methods

	void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    #endregion

    #region Custom Methods
    #endregion
}
