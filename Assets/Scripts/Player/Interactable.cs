using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
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

    public virtual void Awake()
	{
        //Change number 8 to Interactable layer number (Subject to change)
        gameObject.layer = 8;
	}

    #endregion

    #region Custom Methods
    public abstract void OnInteract();
    public abstract void OnFocus();
    public abstract void OnLoseFocus();
    #endregion
}
