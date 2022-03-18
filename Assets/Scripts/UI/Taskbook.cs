using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Taskbook
{
    public Text descriptionText;
    public Image Tick;
    public Image Cross;
    public Image Statusborder;
    public bool isActive;
    public int TaskNumber;

    public void Activate(string descText,int taskNumber)
    {
        descriptionText.text = descText;
        isActive = true;
        Statusborder.gameObject.SetActive(true);
        TaskNumber = taskNumber;
    }

    public void Deactivate()
    {
        descriptionText.text = "";
        isActive = false;
        Statusborder.gameObject.SetActive(false);
        Tick.gameObject.SetActive(false);
        Cross.gameObject.SetActive(false);
    }

    public void Win()
    {
        Tick.gameObject.SetActive(true);
        Cross.gameObject.SetActive(false);
    }

    public void Lose()
    {
        Cross.gameObject.SetActive(true);
        Tick.gameObject.SetActive(false);
    }
}
