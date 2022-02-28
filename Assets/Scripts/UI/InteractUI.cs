using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractUI : MonoBehaviour
{
    #region Variables
    public TMP_Text InteractionText;
    //public Animator TextAnimator;

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
    public void SetInteractText(string TextToSet)
	{
        //InteractionText.SetText(TextToSet);
        InteractionText.text = TextToSet;
        //TextAnimator.SetTrigger("FadeIn");
	}

    public void RemoveInteractText()
	{
       // TextAnimator.SetTrigger("FadeOut");
    }
    #endregion
}
