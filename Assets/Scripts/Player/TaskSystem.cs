using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskSystem : MonoBehaviour
{
    //Player Tasks
    public FirstPersonController player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerProgress();
    }

    private void CheckPlayerProgress()
    {
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            //Total player.tasks
            for (int q = 0; q < player.task.Length; q++)
            {
                //Checking if task is active and in progress(given to player in task book) and is it completed or nor
                if (player.task[q].isActive && player.task[q].inProgress && player.task[q].Completed == false)
                {
                    //Pickup player.tasks
                    if (player.task[q].goal.goalType == GoalType.pick)
                    {
                        if (player.heldObj != null)
                        {
                            /*//Take the feeder from fridge -> player.task[1]
                            if (player.heldObj.name == "Feeder")
                            {
                                player.task[0].Win = true;
                                player.task[0].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }

                            //Pickup plates from the table -> player.task[7]
                            if (player.heldObj.name == "Plate")
                            {
                                player.task[6].Win = true;
                                player.task[6].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }*/

                            //Pickup Baby shirt -> Task[1]
                            if (player.heldObj.name == "Shirt")
                            {
                                player.task[1].Win = true;
                                player.task[1].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            //Clean the TV Lounge -> player.task[0]
                            if (GameObject.FindGameObjectsWithTag("Garbage").Length == 0)
                            {
                                player.task[0].Win = true;
                                player.task[0].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }
                        }
                    }

                    //Give player.tasks
                    if (player.task[q].goal.goalType == GoalType.give && player.SimpleInteractText.text != null && player.GiveButtonPressed == true && player.heldObj != null)
                    {
                        /*//Feed the baby -> player.task[2]
                        if (player.heldObj.name == "Feeder" && player.SimpleInteractText.text == "Feed the baby")
                        {
                            player.task[1].Win = true;
                            player.task[1].Completed = true;
                            player.GiveButtonPressed = false;
                            player.Alert.gameObject.SetActive(true);
                        }

                        //Change baby diaper -> player.task[4]
                        if (player.heldObj.name == "Diaper" && player.SimpleInteractText.text == "Change baby diaper")
                        {
                            player.task[3].Win = true;
                            player.task[3].Completed = true;
                            player.GiveButtonPressed = false;
                            player.Alert.gameObject.SetActive(true);
                        }

                        //Wash Dish -> player.task[8]
                        if (player.heldObj.name == "Plate" && player.SimpleInteractText.text == "Wash Plate")
                        {
                            player.task[7].Win = true;
                            player.task[7].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                            player.GiveButtonPressed = false;
                        }*/

                        //Wash baby shirt in washing machine -> player.task[2]
                        if (player.heldObj.name == "Shirt" && player.SimpleInteractText.text == "Washing Machine")
                        {
                            player.task[2].Win = true;
                            player.task[2].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                            player.GiveButtonPressed = false;
                        }
                    }

                    //Goto player.tasks
                    if (player.task[q].goal.goalType == GoalType.GoTo)
                    {
                        /*//Take baby to washroom -> player.task[3]
                        if (Baby.connectedMassScale == 5 && gotoPoints.pointName == "washroom")
                        {
                            player.task[2].Win = true;
                            player.task[2].Completed = true;
                            gotoPoints.pointName = null;
                            player.Alert.gameObject.SetActive(true);
                        }

                        //Take baby to bedroom -> player.task[5] 6,8,9,10
                        if (Baby.connectedMassScale == 5 && gotoPoints.pointName == "bedroom")
                        {
                            player.task[4].Win = true;
                            player.task[4].Completed = true;
                            gotoPoints.pointName = null;
                            player.Alert.gameObject.SetActive(true);
                        }*/
                    }
                    
                    //Sit player.task
                    if (player.task[q].goal.goalType == GoalType.sit)
                    {
                        /*if (canMove == false)
                        {
                            player.task[8].Win = true;
                            player.task[8].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                        }*/
                    }
                    
                    //Watch player.task
                    if (player.task[q].goal.goalType == GoalType.watch)
                    {
                        /*if (player.NoSignalTV.activeSelf)
                        {
                            player.task[9].Win = true;
                            player.task[9].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                        }*/
                    }
                }
            }
        }
    }
    
}
