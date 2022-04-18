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
    public GameObject Toys,Plates,Key,Feeder,Sink,MagicWand,Book,Souls,holySoul;

    //Baby Animator
    public BabyAnim BabyAnimate;

    //Mirror
    public Mirror mirror;

    //Baby Teleported
    private bool BabyTPCheck;
    //Baby Hide&Seek
    private bool BabyHideAndSeek;
    //BabyLook
    private bool BabyLook;

    //BabyRoom Door
    public Door BabyDoor;

    //Time
    public TimerController Timer;
    public GameObject TimeText;

    //Level 4 Last Mission Condition Run Check
    private bool lvl4ConditionRan, PlayerMoving;

    // Start is called before the first frame update
    void Start()
    {
        BabyTPCheck = false;
        BabyHideAndSeek = false;
        BabyLook = false;
        lvl4ConditionRan = false;
        PlayerMoving = false;
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

        if (BabyLook && !player.BabyTP.isCoroutineReady)
        {
            StartCoroutine(player.BabyTP.BabyLookAtPlayer());
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
                                TaskStatus( 9, true);
                            }

                            //Pickup Baby shirt -> Task[1]
                            if (player.heldObj.name == "Shirt" && !player.task[1].Completed)
                            {
                                TaskStatus( 1, true);
                            }

                            //Find the key in parent's room -> Task[6]
                            if (player.heldObj.name == "Key" && q==6)
                            {
                                TaskStatus( 6, true);
                            }
                        }
                        else
                        {
                            //Clean the TV Lounge -> player.task[0]
                            if (GameObject.FindGameObjectsWithTag("Garbage").Length == 0 && q == 0)
                            {
                                TaskStatus( 0, true);
                            }
                            //Gather all baby toys in basket -> player.task[3]
                            if (ToyBasket.countToys>=3 && player.task[2].Completed && !player.task[3].Completed)
                            {
                                TaskStatus( 3, true);

                                //Spawing Plates
                                Plates.SetActive(true);
                                //ObjectSpawn Sound
                                Sounds.PlaySound("ObjectSpawn");
                            }
                            //Wash Dish -> player.task[4]
                            if (FindLengthOfObject("Plate", 0) == 0 && q == 4 && !player.task[4].Completed)
                            {
                                TaskStatus( 4, true);
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
                            TaskStatus( 10, true);
                            player.GiveButtonPressed = false;
                        }
                        
                        //Change baby diaper -> player.task[12]
                        if (player.heldObj.name == "Diaper" && player.SimpleInteractText.text == "Change baby diaper")
                        {
                            TaskStatus( 12, true);
                            player.GiveButtonPressed = false;
                        }
                        
                        //Wash baby shirt in washing machine -> player.task[2]
                        if (player.heldObj.name == "Shirt" && player.SimpleInteractText.text == "Washing Machine" && !player.task[2].Completed)
                        {
                            TaskStatus( 2, true);
                            player.GiveButtonPressed = false;

                            //Spawing Toys
                            Toys.SetActive(true);
                            //ObjectSpawn Sound
                            Sounds.PlaySound("ObjectSpawn");
                        }

                        //Unlock baby room with key -> player.task[7]
                        if (player.heldObj.name == "Key" && player.SimpleInteractText.text == "Locked")
                        {
                            TaskStatus( 7, true);
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
                            TaskStatus( 11, true);
                            gotoPoints.pointName = null;
                        }

                        //Goto baby room and check the baby -> player.task[5]
                        if (gotoPoints.pointName == "Baby Room" && !player.task[5].Completed && player.task[4].Completed)
                        {
                            TaskStatus( 5, true);
                            gotoPoints.pointName = null;

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
                            TaskStatus( 8, true);
                            gotoPoints.pointName = null;

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
                            TaskStatus( 15, true);
                        }
                    }

                    //Drop player.task
                    if (player.task[q].goal.goalType == GoalType.drop)
                    {
                        //Take baby to the baby room and drop him in cradle -> player.task[13]
                        if (Craddle.BabyinCraddle==true && !player.task[13].Completed && q==13)
                        {
                            TaskStatus( 13, true);
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
                            TaskStatus( 14, true);
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
                                TaskStatus( 0, true);
                            }

                            //Find the magic wand -> Task[8]
                            if (player.heldObj.name == "Magic Wand" && !player.task[8].Completed)
                            {
                                TaskStatus( 8, true);
                            }

                            //Pick up the book -> Task[10]
                            if (player.heldObj.name == "Read Book" && !player.task[10].Completed)
                            {
                                TaskStatus( 10, true);

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
                                TaskStatus( 11, true);
                            }
                        }
                    }

                    //Give player.tasks
                    if (player.task[q].goal.goalType == GoalType.give && player.SimpleInteractText.text != null && player.GiveButtonPressed == true && player.heldObj != null)
                    {
                        //Feed the baby -> player.task[1]
                        if (player.heldObj.name == "Feeder" && player.SimpleInteractText.text == "Feed the baby")
                        {
                            TaskStatus( 1, true);
                            player.GiveButtonPressed = false;
                        }
                        
                        //Change baby diaper -> player.task[3]
                        if (player.heldObj.name == "Diaper" && player.SimpleInteractText.text == "Change baby diaper")
                        {
                            TaskStatus( 3, true);
                            player.GiveButtonPressed = false;
                        }

                        //Use magic wand on baby to make him normal -> player.task[9]
                        if (player.heldObj.name == "Magic Wand" && player.SimpleInteractText.text == "Use magic wand")
                        {
                            TaskStatus( 9, true);
                            player.GiveButtonPressed = false;

                            //Spawn Book
                            Book.SetActive(true);

                            //Changing Mirror to NormalMirror
                            mirror.MirrorChangeToNormal();
                        }

                    }

                    //Goto player.tasks
                    if (player.task[q].goal.goalType == GoalType.GoTo)
                    {
                        //Take baby to washroom -> player.task[2]
                        if (player.Baby.connectedMassScale == 5 && gotoPoints.pointName == "washroom")
                        {
                            TaskStatus( 2, true);
                            gotoPoints.pointName = null;
                        }
                    }

                    //Drop player.task
                    if (player.task[q].goal.goalType == GoalType.drop)
                    {
                        //Take baby to the baby room and drop him in cradle -> player.task[4]
                        if (Craddle.BabyinCraddle == true && !player.task[4].Completed && q == 4)
                        {
                            TaskStatus( 4, true);
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
                            TaskStatus( 5, true);
                        }
                    }

                    //Door player.task
                    if (player.task[q].goal.goalType == GoalType.Door)
                    {
                        //Close the door -> player.task[6]
                        if (GameObject.Find("Baby Room").GetComponent<Door>().isOpen==false && !player.task[6].Completed && q == 6 && player.task[5].Completed)
                        {
                            TaskStatus( 6, true);

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
                            TaskStatus( 7, true);
                            player.GiveButtonPressed = false;

                            //Changing Mirror to HorrorMirror
                            mirror.MirrorChangeToHorror();

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
                                TaskStatus( 1, true);
                            }
                        }
                        else
                        {
                            //Collect all baby toys before time runs out and Dont move while baby is watching you -> player.task[3]
                            //Checking if player has collected all the toys or not
                            if (GameObject.FindGameObjectsWithTag("Toys").Length == 0 && !player.task[3].Completed && player.task[2].Completed)
                            {
                                TaskStatus( 3, true);
                                TimeText.SetActive(false);
                                //Deactivating Timer
                                Timer.GetComponent<TimerController>().enabled = false;
                                //Disabling BabyLook
                                BabyLook = false;
                                //Unpining Baby Weight
                                player.BabyTP.UnpinBabyWeight();
                            }
                            
                            //Checking if time has ran out and if the player failed to collect all the toys
                            else if (Timer.timeValue == 0 && !player.task[3].Completed && player.task[2].Completed && GameObject.FindGameObjectsWithTag("Toys").Length != 0)
                            {
                                TaskStatus( 3, false);
                                TimeText.SetActive(false);
                                //Deactivating Timer
                                Timer.GetComponent<TimerController>().enabled = false;
                                //Disabling BabyLook
                                BabyLook = false;
                                //Unpining Baby Weight
                                player.BabyTP.UnpinBabyWeight();
                            }

                            else if (!player.task[3].Completed && player.BabyTP.BabyisLookingPlayer == true && player.task[2].Completed)
                            {
                                StartCoroutine(isPlayerMoving());

                                if (PlayerMoving)
                                {
                                    TaskStatus( 3, false);
                                    TimeText.SetActive(false);
                                    //Deactivating Timer
                                    Timer.GetComponent<TimerController>().enabled = false;
                                    //Disabling BabyLook
                                    BabyLook = false;
                                    //Unpining Baby Weight
                                    player.BabyTP.UnpinBabyWeight();
                                }
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
                            TaskStatus( 2, true);

                            //Spawning Toys
                            Toys.SetActive(true);

                            //Activating Level 4 Final Mission
                            gotoPoints.pointName = "";

                            //Activating Timer
                            Timer.GetComponent<TimerController>().enabled = true;
                            //Activating Time Text
                            TimeText.SetActive(true);
                            //Droping the baby
                            player.Baby.connectedMassScale = 0;
                            //Teleporting Baby To BabyRoom
                            player.BabyTP.TeleportBabyWithPinWeight();
                        }
                    }

                    //Goto player.tasks
                    if (player.task[q].goal.goalType == GoalType.GoTo)
                    {
                        //Take baby to washroom -> player.task[0]
                        if (player.Baby.connectedMassScale == 5 && gotoPoints.pointName == "washroom")
                        {
                            TaskStatus( 0, true);
                            gotoPoints.pointName = null;
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

                    //Activating Level 4 Final Mission (Collect all baby toys before time runs out and Dont move while baby is watching you)
                    if (gotoPoints.pointName == "Insidebabyroom" && !lvl4ConditionRan)
                    {
                        //Enabling BabyLook
                        BabyLook = true;
                        lvl4ConditionRan = true;
                    }
                }
            }
        }

        else if (SceneManager.GetActiveScene().name == "Level 5")
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
                        //Find and collect 7 souls in soul basket -> player.task[0]
                        if (SoulBasket.countSouls >= 5 && !player.task[0].Completed)
                        {
                            TaskStatus(0, true);

                            //Activating Magic Wand
                            MagicWand.SetActive(true);
                        }
                        if (player.heldObj != null)
                        {
                            //Find the magic wand -> Task[1]
                            if (player.heldObj.name == "Magic Wand" && !player.task[1].Completed)
                            {
                                TaskStatus(1, true);

                                //Teleporting Baby to Baby Chair
                                player.BabyTP.BabyTPSit();
                            }
                        }     
                    }

                    //Give player.tasks
                    if (player.task[q].goal.goalType == GoalType.give && player.SimpleInteractText.text != null && player.GiveButtonPressed == true && player.heldObj != null)
                    {
                        //Turn souls into holly souls -> player.task[2]
                        if (player.heldObj.name == "Magic Wand" && player.SimpleInteractText.text == "Turn into holy soul" && !player.task[2].Completed)
                        {
                            TaskStatus(2, true);
                            player.GiveButtonPressed = false;

                            //Deactivating Souls
                            Souls.SetActive(false);
                            //Activating holy soul
                            holySoul.SetActive(true);
                        }

                        //Use magic wand on baby to make him normal -> player.task[3]
                        if (player.heldObj.name == "Magic Wand" && player.SimpleInteractText.text == "Use magic wand" && !player.task[3].Completed)
                        {
                            TaskStatus(3, true);
                            player.GiveButtonPressed = false;
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

    //1s Delay to inspect if player is moving or not
    IEnumerator isPlayerMoving()
    {
        enabled = false;

        yield return new WaitForSeconds(1);

        PlayerMoving = player.BabyTP.Inspect();

        enabled = true;
    }

    private void TaskStatus(int TaskNumber,bool Won)
    {
        if (Won)
            player.task[TaskNumber].Win = true;
        else
            player.task[TaskNumber].Lose = true;

        player.task[TaskNumber].Completed = true;
        player.Alert.gameObject.SetActive(true);
    }

}
