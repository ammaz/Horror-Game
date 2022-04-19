using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
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
