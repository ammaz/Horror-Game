using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskSystem : MonoBehaviour
{
    //Player Tasks
    public FirstPersonController player;
    public GameObject Baby1;

    //Spawn Objects
    public GameObject Toys,Plates,Key,Feeder,Sink,MagicWand,Book,Souls;

    //Baby Animator
    public BabyAnim BabyAnimate;

    //Baby Teleported
    private bool BabyTPCheck;
    //Baby Hide&Seek
    private bool BabyHideAndSeek;

    //BabyRoom Door
    public Door BabyDoor;

    //Time
    public TimerController Timer;
    public GameObject TimeText;

    // Start is called before the first frame update
    void Start()
    {
        BabyTPCheck = false;
        BabyHideAndSeek = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerProgress();

        //BabyHide&Seek
        if (BabyHideAndSeek)
        {
            player.BabyTP.BabyHideAndSeek();
        }
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
                            if (ToyBasket.countToys>=3 && player.task[2].Completed && !player.task[3].Completed)
                            {
                                player.task[3].Win = true;
                                player.task[3].Completed = true;
                                player.Alert.gameObject.SetActive(true);

                                //Spawing Plates
                                Plates.SetActive(true);
                                //ObjectSpawn Sound
                                Sounds.PlaySound("ObjectSpawn");
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
                        if (player.heldObj.name == "Shirt" && player.SimpleInteractText.text == "Washing Machine" && !player.task[2].Completed)
                        {
                            player.task[2].Win = true;
                            player.task[2].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                            player.GiveButtonPressed = false;

                            //Spawing Toys
                            Toys.SetActive(true);
                            //ObjectSpawn Sound
                            Sounds.PlaySound("ObjectSpawn");
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
                            //Animation Play (Baby Cry)
                            //Teleporting Baby 
                            player.Baby.connectedMassScale = 0;
                            Baby1.transform.position= new Vector3(6.789f, 2f, 2.339f);
                            
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
                            BabyDoor.PlaySound("HorrorDoorKnock");
                            
                            //Spawning Key
                            Key.SetActive(true);
                            //ObjectSpawn Sound
                            Sounds.PlaySound("ObjectSpawn");
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
                            //ObjectSpawn Sound
                            Sounds.PlaySound("ObjectSpawn");
                        }
                    }

                    //Watch player.task
                    if (player.task[q].goal.goalType == GoalType.watch)
                    {
                        if (player.SimpleTV.activeSelf && !player.task[15].Completed && q==15)
                        {
                            player.task[15].Win = true;
                            player.task[15].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                        }
                    }

                    //Drop player.task
                    if (player.task[q].goal.goalType == GoalType.drop)
                    {
                        //Take baby to the baby room and drop him in cradle -> player.task[13]
                        if (Craddle.BabyinCraddle==true && !player.task[13].Completed && q==13)
                        {
                            player.task[13].Win = true;
                            player.task[13].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                            //BabyHappy Sound
                            BabySound.PlaySound("BabyHappy");
                            //Animation Play (Baby Happy)
                            BabyAnimate.BabyHappy();
                        }
                    }

                    //Light player.task
                    if (player.task[q].goal.goalType == GoalType.Light)
                    {
                        //Turn off the lights -> player.task[14]
                        if (player.Switch.GetBool("Off") && !player.task[14].Completed && q == 14)
                        {
                            player.task[14].Win = true;
                            player.task[14].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }

        else if (SceneManager.GetActiveScene().name == "Level 3")
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
                            //Take the feeder from fridge -> player.task[0]
                            if (player.heldObj.name == "Feeder" && !player.task[0].Completed)
                            {
                                player.task[0].Win = true;
                                player.task[0].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }

                            //Find the magic wand -> Task[8]
                            if (player.heldObj.name == "Magic Wand" && !player.task[8].Completed)
                            {
                                player.task[8].Win = true;
                                player.task[8].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }

                            //Pick up the book -> Task[10]
                            if (player.heldObj.name == "Read Book" && !player.task[10].Completed)
                            {
                                player.task[10].Win = true;
                                player.task[10].Completed = true;
                                player.Alert.gameObject.SetActive(true);

                                //Destroying the book
                                Destroy(player.heldObj,0.1f);

                                //Spawn Souls
                                Souls.SetActive(true);
                            }
                            if (player.heldObj.name == "Soul")
                            {
                                //Object Destroy Particle Effects
                                //Object Destroy Sound
                                //Destroying Souls
                                Destroy(player.heldObj, 0.1f);
                            }
                        }
                        else
                        {
                            //Find and collect 7 souls in the house -> player.task[11]
                            if (FindLengthOfObject("Soul", 0) == 0 && !player.task[11].Completed && q==11 && player.task[10].Completed)
                            {
                                player.task[11].Win = true;
                                player.task[11].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }
                        }
                    }

                    //Give player.tasks
                    if (player.task[q].goal.goalType == GoalType.give && player.SimpleInteractText.text != null && player.GiveButtonPressed == true && player.heldObj != null)
                    {
                        //Feed the baby -> player.task[1]
                        if (player.heldObj.name == "Feeder" && player.SimpleInteractText.text == "Feed the baby")
                        {
                            player.task[1].Win = true;
                            player.task[1].Completed = true;
                            player.GiveButtonPressed = false;
                            player.Alert.gameObject.SetActive(true);
                        }
                        
                        //Change baby diaper -> player.task[3]
                        if (player.heldObj.name == "Diaper" && player.SimpleInteractText.text == "Change baby diaper")
                        {
                            player.task[3].Win = true;
                            player.task[3].Completed = true;
                            player.GiveButtonPressed = false;
                            player.Alert.gameObject.SetActive(true);
                        }

                        //Use magic wand on baby to make him normal -> player.task[9]
                        if (player.heldObj.name == "Magic Wand" && player.SimpleInteractText.text == "Use magic wand")
                        {
                            player.task[9].Win = true;
                            player.task[9].Completed = true;
                            player.GiveButtonPressed = false;
                            player.Alert.gameObject.SetActive(true);

                            //Spawn Book
                            Book.SetActive(true);
                        }

                    }

                    //Goto player.tasks
                    if (player.task[q].goal.goalType == GoalType.GoTo)
                    {
                        //Take baby to washroom -> player.task[2]
                        if (player.Baby.connectedMassScale == 5 && gotoPoints.pointName == "washroom")
                        {
                            player.task[2].Win = true;
                            player.task[2].Completed = true;
                            gotoPoints.pointName = null;
                            player.Alert.gameObject.SetActive(true);
                        }
                    }

                    //Drop player.task
                    if (player.task[q].goal.goalType == GoalType.drop)
                    {
                        //Take baby to the baby room and drop him in cradle -> player.task[4]
                        if (Craddle.BabyinCraddle == true && !player.task[4].Completed && q == 4)
                        {
                            player.task[4].Win = true;
                            player.task[4].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                            //BabyHappy Sound
                            BabySound.PlaySound("BabyHappy");
                            //Animation Play (Baby Happy)
                            BabyAnimate.BabyHappy();
                        }
                    }

                    //Light player.task
                    if (player.task[q].goal.goalType == GoalType.Light)
                    {
                        //Turn off the lights -> player.task[14]
                        if (player.Switch.GetBool("Off") && !player.task[5].Completed && q == 5)
                        {
                            player.task[5].Win = true;
                            player.task[5].Completed = true;
                            player.Alert.gameObject.SetActive(true);
                        }
                    }

                    //Door player.task
                    if (player.task[q].goal.goalType == GoalType.Door)
                    {
                        //Close the door -> player.task[6]
                        if (GameObject.Find("Baby Room").GetComponent<Door>().isOpen==false && !player.task[6].Completed && q == 6 && player.task[5].Completed)
                        {
                            player.task[6].Win = true;
                            player.task[6].Completed = true;
                            player.Alert.gameObject.SetActive(true);

                            //Changing Washroom sink name
                            Sink.name = "Wash face";
                        }
                    }

                    //Wash Face player.task
                    if(player.task[q].goal.goalType == GoalType.wash)
                    {
                        //Go downstairs to washroom and wash your face -> player.task[7]
                        if (player.SimpleInteractText.text == "Wash face" && !player.task[7].Completed && player.GiveButtonPressed == true)
                        {
                            player.task[7].Win = true;
                            player.task[7].Completed = true;
                            player.GiveButtonPressed = false;
                            player.Alert.gameObject.SetActive(true);

                            //Changing Washroom sink name
                            Sink.name = "Sink";

                            //Spawing Magic Wand
                            MagicWand.SetActive(true);
                            //ObjectSpawn Sound
                            Sounds.PlaySound("ObjectSpawn");

                            //Baby Unpick
                            player.Baby.connectedMassScale = 0;
                            //Baby Sit Animation
                            BabyAnimate.BabyCry();
                            //BabyTPSit
                            player.BabyTP.BabyTP(-3.25f, 1.298f, -7.021f);
                            //BabyHappy Sound
                            BabySound.PlaySound("BabyCry");
                        }
                    }

                    //Teleporting Baby to Dinning Room
                    if(player.task[2].Completed && player.heldObj !=null && BabyTPCheck == false)
                    {
                        if(player.heldObj.name == "Diaper")
                        {
                            //Baby Unpick
                            player.Baby.connectedMassScale = 0;
                            //Baby Sit Animation
                            BabyAnimate.BabyCry();
                            //BabyTPSit
                            player.BabyTP.BabyTP(-3.25f, 1.298f, -7.021f);
                            //BabyHappy Sound
                            BabySound.PlaySound("BabyCry");
                            //BabyTPCheck turning to true
                            BabyTPCheck = true;
                        }
                    }
                }
            }
        }

        else if (SceneManager.GetActiveScene().name == "Level 4")
        {
            //Destroying the toys
            if (player.heldObj != null && player.heldObj.CompareTag("Toys"))
            {
                Destroy(player.heldObj, 0.1f);
            }

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
                            //Change baby diaper -> player.task[1]
                            if (player.heldObj.name == "Diaper" && !player.task[1].Completed)
                            {
                                player.task[1].Win = true;
                                player.task[1].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            //Collect all baby toys before time runs out and Dont move while baby is watching you -> player.task[3]
                            if (GameObject.FindGameObjectsWithTag("Toys").Length == 0 && !player.task[3].Completed && player.task[2].Completed)
                            {
                                player.task[3].Win = true;
                                player.task[3].Completed = true;
                                player.Alert.gameObject.SetActive(true);
                                TimeText.SetActive(false);
                                //Deactivating Timer
                                Timer.GetComponent<TimerController>().enabled = false;

                            }
                        }
                    }

                    //HideAndSeek player.tasks
                    if(player.task[q].goal.goalType == GoalType.HideAndSeek)
                    {
                        if (Baby.BabyNumHideSeek == 4 && BabyHideAndSeek)
                        {
                            BabyHideAndSeek = false;
                        }

                        if(player.Baby.connectedMassScale==5 && BabyHideAndSeek == false && !player.task[2].Completed && player.task[1].Completed)
                        {
                            player.task[2].Win = true;
                            player.task[2].Completed = true;
                            player.Alert.gameObject.SetActive(true);

                            //Spawning Toys
                            Toys.SetActive(true);
                            //Activating Timer
                            Timer.GetComponent<TimerController>().enabled = true;
                            //Activating Time Text
                            TimeText.SetActive(true);
                        }
                    }

                    //Goto player.tasks
                    if (player.task[q].goal.goalType == GoalType.GoTo)
                    {
                        //Take baby to washroom -> player.task[0]
                        if (player.Baby.connectedMassScale == 5 && gotoPoints.pointName == "washroom")
                        {
                            player.task[0].Win = true;
                            player.task[0].Completed = true;
                            gotoPoints.pointName = null;
                            player.Alert.gameObject.SetActive(true);
                        }
                    }

                    //Teleporting Baby to Dinning Room
                    if (player.task[1].Completed && player.heldObj != null && BabyHideAndSeek == false)
                    {
                        if (player.heldObj.name == "Diaper")
                        {
                            //Baby Unpick
                            player.Baby.connectedMassScale = 0;
                            //Destroying Diaper
                            Destroy(player.heldObj,0.1f);
                            //BabyHappy Animation
                            BabyAnimate.BabyHappy();
                            //BabyHide&Seek
                            BabyHideAndSeek = true;
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
