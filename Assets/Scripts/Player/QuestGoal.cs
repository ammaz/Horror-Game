using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

   /* public bool Failed;
    public bool Success;
    public bool Completed;

    private Quest[] task = new Quest[10];

    public void Pick(GameObject objectName)
    {
        if (goalType == GoalType.pick && objectName != null)
        {
            if (objectName.name == "Feeder")
            {
                task[0].goal.Success = true;
                task[0].goal.Failed = false;
            }

            if (objectName.name == "Plates")
            {
                task[6].goal.Success = true;
                task[6].goal.Failed = false;
            }
        }  
    }

    public void Give(GameObject pickedObject,string text,bool feederCheck)
    {
        if (goalType == GoalType.give && pickedObject!=null && text!=null)
        {
            //Baby layer 12 (Change the number if will change baby layer)
            if(pickedObject.name == "Feeder" && text=="Baby" && feederCheck==true)
            {
                task[2].goal.Success = true;
                task[2].goal.Failed = false;
            }
        }
    }*/
}

public enum GoalType
{
    pick,
    give,
    drop,
    wash,
    sit,
    watch,
    Light,
    GoTo,
    Door,
    HideAndSeek
}
