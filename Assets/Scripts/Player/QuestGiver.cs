using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestGiver : MonoBehaviour
{
    public Quest[] quest;

    public FirstPersonController player;

    public GameObject questWindow;

    public Text LevelName;

    public Taskbook[] TaskList;

    void Start()
    {
        LevelName.text = "" + SceneManager.GetActiveScene().name;
        OpenQuestWindow();
    }

    //Assign to TaskBook Button
    public void OpenQuestWindow()
    {
        for (int a = 0; a < quest.Length; a++)
        {
            if (quest[a].isActive == true && quest[a].inProgress == false)
            {
                int check = 0;
                //For Deactivating
                for (int b = 0; b < TaskList.Length; b++)
                {
                    //Checking if task is completed or not (By tick or cross)
                    if (TaskList[b].Tick.gameObject.activeSelf == true || TaskList[b].Cross.gameObject.activeSelf == true)
                    {
                        check++;
                    }
                    if (check == 4)
                    {
                        for (int c = 0; c < 4; c++)
                        {
                            TaskList[c].Deactivate();
                            quest[TaskList[c].TaskNumber - 1].inProgress = false;
                            quest[TaskList[c].TaskNumber - 1].isActive = false;
                        }
                        check = 0;
                    }
                }
                //For Activating 
                for (int b = 0; b < TaskList.Length; b++)
                {
                    if (TaskList[b].isActive == false && quest[a].inProgress == false)
                    {
                        TaskList[b].Activate(quest[a].description, quest[a].taskNumber);
                        quest[a].inProgress = true;
                        player.task[a] = quest[a];
                    }
                }
            }

            //Activating Win or lose Image
            if (quest[a].isActive == true && quest[a].inProgress == true)
            {
                if (quest[a].Win == true)
                {
                    for (int d = 0; d < TaskList.Length; d++)
                    {
                        if (TaskList[d].TaskNumber == quest[a].taskNumber)
                        {
                            TaskList[d].Win();
                        }
                    }
                }
                if (quest[a].Lose == true)
                {
                    for (int d = 0; d < TaskList.Length; d++)
                    {
                        if (TaskList[d].TaskNumber == quest[a].taskNumber)
                        {
                            TaskList[d].Lose();
                        }
                    }
                }
            }
            player.task[a] = quest[a];
        }
    }
}
