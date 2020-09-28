using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    //Fields
    public TextMeshProUGUI dayAtWork;
    public TextMeshProUGUI NPCtalking;
    public Button choice1;
    public Button choice2;
    public Button helloButton;

    public GameObject NPCManager;
    public GameObject player;

    private NpcManager npcManagerScript;
    private PlayerControls playerScript;

    private bool buttonOneClick;
    private bool buttonTwoClick;
    private bool helloButtonClick;
    private bool doneTalking;
    private bool saidHello;
    private bool kind;
    private bool responsible;
    private bool firstPass;

    int choice1Index;
    int choice2Index;
    int npcTalkingIndex;

    string[] playerDialogue;
    string[] npcDialogue;

    /// <summary>
    /// Check to see if the player is done talking to the NPC
    /// </summary>
    public bool DoneTalking
    {
        get { return doneTalking; }
    }

    
    void Start()
    {
        //initailization
        npcManagerScript = NPCManager.GetComponent<NpcManager>();
        playerScript = player.GetComponent<PlayerControls>();

        choice1.onClick.AddListener(OnButtonOneClick);
        choice2.onClick.AddListener(OnButtonTwoClick);
        helloButton.onClick.AddListener(OnHelloButtonClick);

        NPCtalking.gameObject.SetActive(false);
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        helloButton.gameObject.SetActive(false);

        doneTalking = false;
        buttonOneClick = false;
        buttonTwoClick = false;
        helloButtonClick = false;
        kind = false;
        responsible = false;
        saidHello = false;
        firstPass = true;

        choice1Index = 0;
        choice2Index = 1;
        npcTalkingIndex = 0;

        //ALL DIALOGUE GOES HERE
        playerDialogue = new string[]
        {
            //UNKIND IRRESPONSIBLE
            "Can I help you?",                              //0
            "What seems to be the problem?",                //1
            "Well, I hope you have a nice day...",          //2
            "There's no need to be so rude about it!",      //3
            "I understand, but I can't anything.",          //4
            "Well it's for the benefit of us all!"          //5
        };
        npcDialogue = new string[]
        {
            //DEFAULT NPC START
            "...",                                          //0

            //UNKIND IRRESPONSIBLE
            "Hmph.",                                        //1
            "I doubt it.",                                  //2
            "Whatever.",                                    //3
            "How about you stick it up your ass?",          //4
            "My PROBLEM are these damn masks!",             //5
            "Yeah, well, thanks for nothing.",              //6
            "Forget this! I'm never coming here again!"     //7
        };

        helloButton.GetComponentInChildren<TextMeshProUGUI>().text = "Hello!";
    }


    void Update()
    {
        //checks to see if there is a customer
        if (npcManagerScript.GetCustomer())
        dayAtWork.text = "Days at work: " + Days.day;
        if (npcManagerScript.GetCustomer() == true)
        {
            if (!doneTalking)
            {
                StartDialog();
            }
        }
        else
        {
            //resets everything to default state before a customer arrives
            NPCtalking.gameObject.SetActive(false);
            choice1.gameObject.SetActive(false);
            choice2.gameObject.SetActive(false);
            doneTalking = false;

            choice1Index = 0;
            choice2Index = 1;
            npcTalkingIndex = 0;
            firstPass = true;
            saidHello = false;
        }

        buttonOneClick = false;
        buttonTwoClick = false;
        helloButtonClick = false;
    }

    //main dialogue loop
    void StartDialog()
    {
        //DEBUG - REMOVE AT LATER POINT
        npcManagerScript.Responsibility = 1;
        npcManagerScript.Kindness = 1;
        //END DEBUG

        
        NPCtalking.gameObject.SetActive(true);
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);

        //hello button is the trigger to start the actual dialogue sequence
        if (!saidHello)
        {
            helloButton.gameObject.SetActive(true);
        }
        else
        {
            helloButton.gameObject.SetActive(false);
            if (npcManagerScript.Responsibility < 3)
            {
                NPCtalking.text = "Hello";
                choice2.gameObject.SetActive(true);
            }
        }

        //determines the personality of the NPC
        if (npcManagerScript.Kindness < 3)
            kind = false;
        else
            kind = true;

        if (npcManagerScript.Responsibility < 3)
            responsible = false;
        else
            responsible = true;

        Debug.Log("Kindness: " + npcManagerScript.Kindness + ", Responsibility: " + npcManagerScript.Responsibility);


        if (helloButtonClick || saidHello)
        {
            saidHello = true;
            
            //call a dialogue method depending on what personality the NPC has
            if(!kind && !responsible)
            {
                UnkindIrresponsible();
            }
        }

        //change the text of the NPC and choice buttons accordingly
        choice1.GetComponentInChildren<TextMeshProUGUI>().text = playerDialogue[choice1Index];
        choice2.GetComponentInChildren<TextMeshProUGUI>().text = playerDialogue[choice2Index];
        NPCtalking.text = npcDialogue[npcTalkingIndex];

        //if (npcManagerScript.Kindness < 3)
        //{
        //    if (npcManagerScript.Responsibility < 3)
        //    {
        //        NPCtalking.text = "Hmph.";

        //        if (buttonOneClick)
        //        {
        //            NPCtalking.text = 
        //        }
        //        else if (buttonTwoClick)
        //        {

        //        }
        //    }
        //    else
        //    {
        //        NPCtalking.text = "RWAR MASK BAD";
        //    }
        //}
        //else
        //{
        //    if (npcManagerScript.Responsibility < 3)
        //    {
        //        NPCtalking.text = "Hello";
        //        choice2.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        NPCtalking.text = "Hello";
        //        choice1.GetComponentInChildren<Text>().text = "Test Dialogue";
        //    }
        //}
    }

    //private void first()//this is the response if they are not wearing and you say nothing or they are wearing a mask and is not rude
    //{
    //    doneTalking = true;
    //    if (npcManagerScript.Kindness < 3)
    //    {
    //        NPCtalking.text = "RWWWAAAAARRR AWFUL";
    //        playerScript.Mood = -1;
    //    }
    //    else
    //    {
    //        NPCtalking.text = "Have a nice day";
    //        playerScript.Mood = 1;
    //    }
    //    choice1.gameObject.SetActive(false);
    //    choice2.gameObject.SetActive(false);
    //}

    //private void second()//this response is if they are not wearing a mask you say something
    //{
    //    doneTalking = true;
    //    if (npcManagerScript.Kindness < 3)
    //    {
    //        NPCtalking.text = "RWWWAAARRR NO";
    //        playerScript.Mood = -1;
    //    }
    //    else
    //    {
    //        NPCtalking.text = "Sure";
    //        playerScript.Mood = 1;
    //    }
    //    choice1.gameObject.SetActive(false);
    //    choice2.gameObject.SetActive(false);
    //}

    //private void third()//Wearing a mask and is just plain rude
    //{
    //    doneTalking = true;
    //    NPCtalking.text = "RWAAAR AWFUL";
    //    playerScript.Mood = -1;
    //    choice1.gameObject.SetActive(false);
    //    choice2.gameObject.SetActive(false);
    //}

    //private void fourth()//Wearing a mask and is fine
    //{
    //    doneTalking = true;
    //    NPCtalking.text = "Have a nice day";
    //    playerScript.Mood = 1;
    //    choice1.gameObject.SetActive(false);
    //    choice2.gameObject.SetActive(false);
    //}

    //Dialogue Methods

    private void UnkindIrresponsible()
    {
        choice1.gameObject.SetActive(true);
        choice2.gameObject.SetActive(true);

        if (firstPass)
        {
            npcTalkingIndex = 1;
            firstPass = false;
        }

        if (buttonOneClick)
        {
            switch (choice1Index)
            {
                case 0:
                    npcTalkingIndex = 2;
                    choice1Index = 2;
                    choice2Index = 3;
                    break;
                case 2:
                    //neutral interaction, no mood change
                    npcTalkingIndex = 3;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    break;
                case 4:
                    //slightly negative interaction, -1 mood
                    npcTalkingIndex = 6;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = -1;
                    Debug.Log(playerScript.Mood);
                    break;
                default:
                    break;
            }
        }
        else if (buttonTwoClick)
        {
            switch (choice2Index)
            {
                case 1:
                    npcTalkingIndex = 5;
                    choice1Index = 4;
                    choice2Index = 5;
                    break;
                case 3:
                    //slightly negative interaction, -1 mood
                    npcTalkingIndex = 4;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = -1;
                    Debug.Log(playerScript.Mood);
                    break;
                case 5:
                    //very negative interaction, -3 mood
                    npcTalkingIndex = 7;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = -3;
                    Debug.Log(playerScript.Mood);
                    break;
                default:
                    break;
            }
        }
    }


    //Helper Methods
    private void OnButtonOneClick()
    {
        buttonOneClick = true;
        Debug.Log("Button One Clicked!");
    }

    private void OnButtonTwoClick()
    {
        buttonTwoClick = true;
        Debug.Log("Button Two Clicked!");
    }

    private void OnHelloButtonClick()
    {
        helloButtonClick = true;
        Debug.Log("Hello Button Clicked!");
    }
}
