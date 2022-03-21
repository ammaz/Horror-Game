using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest
{
    public bool isActive;

    public int taskNumber;
    public string description;

    public bool Win;
    public bool Lose;

    public bool inProgress;

    public QuestGoal goal;
}
