using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskSystem : MonoBehaviour
{
    //Player Tasks
    public FirstPersonController player;
    public GameObject Baby;

    //Spawn Objects
    public GameObject Toys,Plates,Key,Feeder;

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
                            //Take the feeder from fridge -> player.task[9]
                            if (player.heldObj.name == "Feeder" && q==9)
                            {
                                player.task[9].Win = true;
                                player.task[9].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }
                            /*
                            //Pickup plates from the table -> player.task[4]
                            if (player.heldObj.name == "Plate")
                            {
                                player.task[4].Win = true;
                                player.task[4].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }*/

                            //Pickup Baby shirt -> Task[1]
                            if (player.heldObj.name == "Shirt" && !player.task[1].Completed)
                            {
                                player.task[1].Win = true;
                                player.task[1].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }

                            //Find the key in parent's room -> Task[6]
                            if (player.heldObj.name == "Key" && q==6)
                            {
                                player.task[6].Win = true;
                                player.task[6].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            //Clean the TV Lounge -> player.task[0]
                            if (GameObject.FindGameObjectsWithTag("Garbage").Length == 0 && q == 0)
                            {
                                player.task[0].Win = true;
                                player.task[0].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }
                            //Gather all baby toys in basket -> player.task[3]
                            if (GameObject.FindGameObjectsWithTag("Toys").Length == 0 && player.task[2].Completed && !player.task[3].Completed)
                            {
                                player.task[3].Win = true;
                                player.task[3].Completed = true;
                                player.Alert.gameObject.SetActive(true);

                                //Spawing Plates
                                Plates.SetActive(true);
                            }
                            //Wash Dish -> player.task[4]
                            if (FindLengthOfObject("Plate", 0) == 0 && q == 4 && !player.task[4].Completed)
                            {
                                player.task[4].Win = true;
                                player.task[4].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                                gotoPoints.pointName = null;
                                player.GiveButtonPressed = false;

                                StartCoroutine(GameObject.Find("Baby Room").GetComponent<Door>().AutoClose());

                            }
                        }
                    }

                    //Give player.tasks
                    if (player.task[q].goal.goalType == GoalType.give && player.SimpleInteractText.text != null && player.GiveButtonPressed == true && player.heldObj != null)
                    {
                        //Feed the baby -> player.task[10]
                        if (player.heldObj.name == "Feeder" && player.SimpleInteractText.text == "Feed the baby")
                        {
                            player.task[10].Win = true;
                            player.task[10].Completed = true;
                            player.GiveButtonPressed = false;
                            player.Alert.gameObject.SetActive(true);
                        }
                        
                        //Change baby diaper -> player.task[12]
                        if (player.heldObj.name == "Diaper" && player.SimpleInteractText.text == "Change baby diaper")
                        {
                            player.task[12].Win = true;
                            player.task[12].Completed = true;
                            player.GiveButtonPressed = false;
                            player.Alert.gameObject.SetActive(true);
                        }
                        
                        //Wash baby shirt in washing machine -> player.task[2]
                        if (player.heldObj.name == "Shirt" && player.SimpleInteractText.text == "Washing Machine")
                        {
                            player.task[2].Win = true;
                            player.task[2].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                            player.GiveButtonPressed = false;

                            //Spawing Toys
                            Toys.SetActive(true);
                        }

                        //Unlock baby room with key -> player.task[7]
                        if (player.heldObj.name == "Key" && player.SimpleInteractText.text == "Locked")
                        {
                            player.task[7].Win = true;
                            player.task[7].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                            player.GiveButtonPressed = false;

                            //Enabling Baby Room Door
                            GameObject.Find("Locked").GetComponent<Animator>().enabled = true;
                            GameObject.Find("Locked").name = "Baby Room";

                            //Audio Play (Baby cry sound)
                            /*
                            //Teleporting Baby 
                            player.Baby.connectedMassScale = 0;
                            Baby.transform.position= new Vector3(6.789f, 1f, 2.339f);
                            */
                        }
                    }

                    //Goto player.tasks
                    if (player.task[q].goal.goalType == GoalType.GoTo)
                    {
                        //Take baby to washroom -> player.task[11]
                        if (player.Baby.connectedMassScale == 5 && gotoPoints.pointName == "washroom" && q==11)
                        {
                            player.task[11].Win = true;
                            player.task[11].Completed = true;
                            gotoPoints.pointName = null;
                            player.Alert.gameObject.SetActive(true);
                        }
                        /*
                        //Take baby to bedroom -> player.task[5] 6,8,9,10
                        if (Baby.connectedMassScale == 5 && gotoPoints.pointName == "bedroom")
                        {
                            player.task[4].Win = true;
                            player.task[4].Completed = true;
                            gotoPoints.pointName = null;
                            player.Alert.gameObject.SetActive(true);
                        }*/
                        //Goto baby room and check the baby -> player.task[5]
                        if (gotoPoints.pointName == "Baby Room" && !player.task[5].Completed && player.task[4].Completed)
                        {
                            player.task[5].Win = true;
                            player.task[5].Completed = true;
                            gotoPoints.pointName = null;
                            player.Alert.gameObject.SetActive(true);

                            //Disabling Baby Room Door
                            GameObject.Find("Baby Room").GetComponent<Animator>().enabled = false;
                            GameObject.Find("Baby Room").name = "Locked";

                            //Audio Play (Knocking on door horror sound)

                            //Spawning Key
                            Key.SetActive(true);
                        }

                        //Run downstairs to find the baby -> player.task[5]
                        if (gotoPoints.pointName=="downstair" && player.task[7].Completed && !player.task[8].Completed)
                        {
                            player.task[8].Win = true;
                            player.task[8].Completed = true;
                            gotoPoints.pointName = null;
                            player.Alert.gameObject.SetActive(true);

                            //Spawing Feeder
                            Feeder.SetActive(true);
                        }
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
                        if (player.NoSignalTV.activeSelf && player.task[12].Completed && q==15)
                        {
                            player.task[15].Win = true;
                            player.task[15].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    private int FindLengthOfObject(string Name, int Sum)
    {
        foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (gameObj.name == Name)
            {
                Sum++;
            }
        }

        return Sum;
    }
    
}
