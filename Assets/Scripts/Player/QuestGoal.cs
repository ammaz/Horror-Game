using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public bool Failed;
    public bool Success;
    public bool Completed;

    public bool IsReached()
    {
        return Completed;
    }

    public void Pick(GameObject objectName)
    {
        if (goalType == GoalType.pick && objectName != null)
        {
            if (objectName.name == "Feeder")
            {
                Success = true;
                Failed = false;
                Completed = true;  
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
                Success = true;
                Failed = false;
                Completed = true;
            }
        }
    }
}

public enum GoalType
{
    pick,
    give,
    drop,
    wash,
    sit,
    watch
}
