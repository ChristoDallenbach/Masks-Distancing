using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text NPCtalking;
    public Button choice1;
    public Button choice2;
    public Button choice3;
    public Button choice4;
    public GameObject NPCManager;
    public GameObject player;

    private bool saidHello;
    private NpcManager npcManagerScript;
    private PlayerControls playerScript;

    // Start is called before the first frame update
    void Start()
    {
        choice1.onClick.AddListener(first);
        choice2.onClick.AddListener(second);
        choice3.onClick.AddListener(third);
        choice4.onClick.AddListener(fourth);
        choice1.GetComponentInChildren<Text>().text = "Hello";
        choice2.GetComponentInChildren<Text>().text = "Plese put mask on";
        choice3.GetComponentInChildren<Text>().text = "Sorry you are suffering";
        choice4.GetComponentInChildren<Text>().text = "Thank you for wearing a mask";
        NPCtalking.gameObject.SetActive(false);
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        choice3.gameObject.SetActive(false);
        choice4.gameObject.SetActive(false);
        saidHello = false;
        npcManagerScript = NPCManager.GetComponent<NpcManager>();
        playerScript = player.GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {

        if (npcManagerScript.getCustomer() == true)
        {
            if (saidHello == false)
            {
                startDialog();
            }
        }
        else
        {
            NPCtalking.gameObject.SetActive(false);
            choice1.gameObject.SetActive(false);
            choice2.gameObject.SetActive(false);
            choice3.gameObject.SetActive(false);
            choice4.gameObject.SetActive(false);
            saidHello = false;
        }
    }

    public bool getSaidHello()
    {
        return saidHello;
    }

    void startDialog()
    {
        NPCtalking.gameObject.SetActive(true);
        choice1.gameObject.SetActive(true);

        if (npcManagerScript.getKindness() < 3)
        {
            if (npcManagerScript.getRespnse() < 3)
            {
                NPCtalking.text = "RWAR MASK BAD";
                choice2.gameObject.SetActive(true);
            }
            else
            {
                NPCtalking.text = "RWAR MASK BAD";
                choice3.gameObject.SetActive(true);
            }
        }
        else
        {
            if (npcManagerScript.getRespnse() < 3)
            {
                NPCtalking.text = "Hello";
                choice2.gameObject.SetActive(true);
            }
            else
            {
                NPCtalking.text = "Hello";
                choice1.GetComponentInChildren<Text>().text = "Hello";
                choice4.gameObject.SetActive(true);
            }
        }
    }

    private void first()//this is the response if they are not wearing and you say nothing or they are wearing a mask and is not rude
    {
        saidHello = true;
        if (npcManagerScript.getKindness() < 3)
        {
            NPCtalking.text = "RWWWAAAAARRR AWFUL";
            playerScript.setMood(-1);
        }
        else
        {
            NPCtalking.text = "Have a nice day";
            playerScript.setMood(1);
        }
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        choice3.gameObject.SetActive(false);
        choice4.gameObject.SetActive(false);
    }

    private void second()//this response is if they are not wearing a mask you say something
    {
        saidHello = true;
        if (npcManagerScript.getKindness() < 3)
        {
            NPCtalking.text = "RWWWAAARRR NO";
            playerScript.setMood(-1);
        }
        else
        {
            NPCtalking.text = "Sure";
            playerScript.setMood(1);
        }
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        choice3.gameObject.SetActive(false);
        choice4.gameObject.SetActive(false);
    }

    private void third()//Wearing a mask and is just plain rude
    {
        saidHello = true;
        NPCtalking.text = "RWAAAR AWFUL";
        playerScript.setMood(-1);
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        choice3.gameObject.SetActive(false);
        choice4.gameObject.SetActive(false);
    }

    private void fourth()//Wearing a mask and is fine
    {
        saidHello = true;
        NPCtalking.text = "Have a nice day";
        playerScript.setMood(1);
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        choice3.gameObject.SetActive(false);
        choice4.gameObject.SetActive(false);
    }
}