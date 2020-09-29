using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
            //UNKIND RESPONSIBLE
            "Can I help you?",                              //0
            "What seems to be the problem?",                //1
            "Well, I hope you have a nice day...",          //2
            "There's no need to be so rude about it!",      //3
            "I understand, but I can't anything.",          //4
            "Well it's for the benefit of us all!",         //5
            //KIND RESPONABLE
            "Did you find everything alright?",             //6
            "Did you have any troubles?",                   //7
            "Thank you for wearing your mask!",             //8
            "Well that's great to hear.",                   //9
            "Yes, that does seem to be an issue",           //10
            "I'm very sorry to hear that!",                 //11
            //KIND IRRESPONSIBLE
            "I'm alright",                                  //12
            "Fine. Could I ask you to put on a mask?",      //13
            "No, I've been stuck here.",                    //14
            "No, We're supposed to be quarantined",         //15
            "Don't worry about it",                         //16
            "Do better next time",                          //17
            //UNKIND IRRESPONSIBLE
            "Did your shopping go well?",                   //18
            "Could I ask you to put on a mask?",            //19
            "Alright, have a nice day",                     //20
            "Do you have a problem?",                       //21
            "OK, I apologize!",                             //22
            "Please leave the store"                        //23
        };
        npcDialogue = new string[]
        {
            //DEFAULT NPC START
            "...",                                          //0

            //UNKIND RESPONSIBLE
            "Hmph.",                                        //1
            "I doubt it.",                                  //2
            "Whatever.",                                    //3
            "How about you stick it up your ass?",          //4
            "My PROBLEM are these damn masks!",             //5
            "Yeah, well, thanks for nothing.",              //6
            "Forget this! I'm never coming here again!",    //7

            //KIND RESPONABLE
            "Hi!",                                          //8
            "I found everything just fine!",                //9
            "Of course!",                                   //10
            "Well, aside from some people not wearing masks, no", //11
            "Hopefully those people will change...",        // 12
            "Hope you stay safe!",                          //13
            "Have a nice day!",                             //14

            //KIND IRRESPNSIBLE
            "How are you?",                                 //15
            "Have you been seeing many friends?",           //16
            "Well, hope you get some time to have fun",     //17
            "Well, that's more of a recommendation",        //18
            "Oh, of course. My apologies",                  //19
            "Thanks, I'll remember next time",              //20
            "I said I was sorry...",                        //21

            //UNKIND IRRESPONSIBLE
            "Hey.",                                         //22
            "How about we don't talk to each other?",       //23
            "Don't tell me what to do",                     //24
            "Just don't talk to me",                        //25
            "Hell no, you can't force me to wear a muzzle!",//26
            "Yeah, that's what I thought",                  //27
            "Gladly. And I ain't coming back!"              //28
        };

        helloButton.GetComponentInChildren<TextMeshProUGUI>().text = "Hello!";
    }


    void Update()
    {
        //checks to see if there is a customer
        if (npcManagerScript.getCustomer())
            dayAtWork.text = "Days at work: " + Days.day;
        if (npcManagerScript.getCustomer() == true)
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
            if (!kind && responsible)
            {
                UnkindResponsible();
            }
            else if (kind && responsible)
            {
                KindResponsible();
            }
            else if (kind && !responsible)
            {
                KindIrresponsible();
            }
            else
            {
                UnkindIrresponsible();
            }
        }

        //change the text of the NPC and choice buttons accordingly
        choice1.GetComponentInChildren<TextMeshProUGUI>().text = playerDialogue[choice1Index];
        choice2.GetComponentInChildren<TextMeshProUGUI>().text = playerDialogue[choice2Index];
        NPCtalking.text = npcDialogue[npcTalkingIndex];

        //Dialogue Methods
    }

    private void UnkindResponsible()
    {
        choice1.gameObject.SetActive(true);
        choice2.gameObject.SetActive(true);

        if (firstPass)
        {
            choice1Index = 0;
            choice2Index = 1;
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
                    amIInfected(1);
                    break;
                case 4:
                    //slightly negative interaction, -1 mood
                    npcTalkingIndex = 6;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = -1;
                    Debug.Log(playerScript.Mood);
                    amIInfected(1);
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
                    amIInfected(1);
                    break;
                case 5:
                    //very negative interaction, -3 mood
                    npcTalkingIndex = 7;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = -3;
                    Debug.Log(playerScript.Mood);
                    amIInfected(1);
                    break;
                default:
                    break;
            }
        }
    }

    private void KindResponsible()
    {
        choice1.gameObject.SetActive(true);
        choice2.gameObject.SetActive(true);

        if (firstPass)
        {
            choice1Index = 6;
            choice2Index = 7;
            npcTalkingIndex = 8;
            firstPass = false;
        }

        if (buttonOneClick)
        {
            switch (choice1Index)
            {
                case 6:
                    npcTalkingIndex = 9;
                    choice1Index = 8;
                    choice2Index = 9;
                    break;
                case 8:
                    //neutral interaction, no mood change
                    npcTalkingIndex = 10;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    amIInfected(1);
                    break;
                case 10:
                    //slightly positive interaction, +1 mood
                    npcTalkingIndex = 12;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = 1;
                    Debug.Log(playerScript.Mood);
                    amIInfected(1);
                    break;
                default:
                    break;
            }
        }
        else if (buttonTwoClick)
        {
            switch (choice2Index)
            {
                case 7:
                    npcTalkingIndex = 11;
                    choice1Index = 10;
                    choice2Index = 11;
                    break;
                case 9:
                    //slightly positive interaction, +1 mood
                    npcTalkingIndex = 14;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = 1;
                    Debug.Log(playerScript.Mood);
                    amIInfected(1);
                    break;
                case 11:
                    //very positive interaction, +3 mood
                    npcTalkingIndex = 13;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = 3;
                    Debug.Log(playerScript.Mood);
                    amIInfected(1);
                    break;
                default:
                    break;
            }
        }
    }

    private void KindIrresponsible()
    {
        choice1.gameObject.SetActive(true);
        choice2.gameObject.SetActive(true);

        if (firstPass)
        {
            choice1Index = 12;
            choice2Index = 13;
            npcTalkingIndex = 15;
            firstPass = false;
        }

        if (buttonOneClick)
        {
            switch (choice1Index)
            {
                case 12:
                    npcTalkingIndex = 16;
                    choice1Index = 14;
                    choice2Index = 15;
                    break;
                case 14:
                    //neutral interaction, no mood change
                    npcTalkingIndex = 17;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    amIInfected(20);
                    break;
                case 16:
                    npcTalkingIndex = 20;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = 2;
                    Debug.Log(playerScript.Mood);
                    amIInfected(10);
                    break;
                default:
                    break;
            }
        }
        else if (buttonTwoClick)
        {
            switch (choice2Index)
            {
                case 13:
                    npcTalkingIndex = 19;
                    choice1Index = 16;
                    choice2Index = 17;
                    break;
                case 15:
                    //slightly negative interaction, -1 mood
                    npcTalkingIndex = 18;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = -1;
                    Debug.Log(playerScript.Mood);
                    amIInfected(20);
                    break;
                case 17:
                    //very negative interaction, -3 mood
                    npcTalkingIndex = 21;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = -3;
                    Debug.Log(playerScript.Mood);
                    amIInfected(10);
                    break;
                default:
                    break;
            }
        }
    }

    private void UnkindIrresponsible()
    {
        choice1.gameObject.SetActive(true);
        choice2.gameObject.SetActive(true);

        if (firstPass)
        {
            choice1Index = 18;
            choice2Index = 19;
            npcTalkingIndex = 22;
            firstPass = false;
        }

        if (buttonOneClick)
        {
            switch (choice1Index)
            {
                case 18:
                    npcTalkingIndex = 23;
                    choice1Index = 20;
                    choice2Index = 21;
                    break;
                case 20:
                    //neutral interaction, no mood change
                    npcTalkingIndex = 24;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    amIInfected(20);
                    break;
                case 22:
                    //slightly negative interaction, -3 mood
                    npcTalkingIndex = 27;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = -3;
                    Debug.Log(playerScript.Mood);
                    amIInfected(20);
                    break;
                default:
                    break;
            }
        }
        else if (buttonTwoClick)
        {
            switch (choice2Index)
            {
                case 19:
                    npcTalkingIndex = 26;
                    choice1Index = 22;
                    choice2Index = 23;
                    break;
                case 21:
                    //slightly negative interaction, -1 mood
                    npcTalkingIndex = 25;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = -1;
                    Debug.Log(playerScript.Mood);
                    amIInfected(20);
                    break;
                case 23:
                    //very negative interaction, -3 mood
                    npcTalkingIndex = 28;
                    doneTalking = true;
                    choice1.gameObject.SetActive(false);
                    choice2.gameObject.SetActive(false);
                    playerScript.Mood = -3;
                    Debug.Log(playerScript.Mood);
                    amIInfected(20);
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


    private void amIInfected(int percent)
    {
        int num = (int)UnityEngine.Random.Range(1, 100); //makes a random number, if it is bellow the percent, you are infected
        if (percent > num)
        {
             Days.infected = true;
        }
    }

}