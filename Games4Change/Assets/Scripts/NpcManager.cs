using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NpcManager : MonoBehaviour
{
    public GameObject customer;
    private GameObject customerClone;
    public GameObject dialog;
    public GameObject[] maskNPC;
    public GameObject[] noMaskNPC;
    private DialogManager dialogScript;
    private int numCustomerInADay;// the number of costemers the player will see through the entire day
    private int CustomerNum;//the number of custemers the player has seen
    private float tempTime = 0.0f;//This is just until scanning is done, don't knw how to check yet
    private float Timer = 0.0f;
    private float timeUntilC = 5.0f;
    private bool customerPresent;
    private bool groceriesBagged;
    private bool spawnGroceries;
    //these are npc stuff
    private int responsibility; //chances of wearing a mask
    private int kindness; //chances on being rude about puting the mask on

    // Getters and Setters for variables
    public int Responsibility
    {
        get { return responsibility; }
        set { responsibility = value; }
    }

    public int Kindness
    {
        get { return kindness; }
        set { kindness = value; }
    }

    public int NumCustomerInADay
    {
        get { return numCustomerInADay; }
        set { numCustomerInADay = Days.day + numCustomerInADay; }
    }

    public bool GroceriesBagged
    {
        get { return groceriesBagged; }
        set { groceriesBagged = value; }
    }

    public bool SpawnGroceries
    {
        get { return spawnGroceries; }
        set { spawnGroceries = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogScript = dialog.GetComponent<DialogManager>();
        customerPresent = false;
        Responsibility = 0;
        Kindness = 0;
        numCustomerInADay = 4;
        CustomerNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        customerTimer();
        if (dialogScript.DoneTalking == true && groceriesBagged)
        {
            tempTime += Time.deltaTime;
            if (tempTime > timeUntilC)
            {
                setCustomerleave();
                tempTime = 0;
            }
        }
    }

    // Timer to check if the new customer should be there yet
    void customerTimer()
    {
        Timer += Time.deltaTime;
        if (Timer > timeUntilC && customerPresent == false)
        {
            customerPresent = true;
            Responsibility = (int)UnityEngine.Random.Range(0, 5);
            Kindness = (int)UnityEngine.Random.Range(0, 5);
            SpawnCustomer();
            spawnGroceries = true;
        }
    }

    // getter for current customer
    public bool getCustomer()
    {
        return customerPresent;
    }

    // method for making the customer leave
    public void setCustomerleave()
    {
        customerPresent = false;
        Timer = 0f;
        Destroy(customerClone);
        if (numCustomerInADay == CustomerNum)
        {
            Days.day += 1;
            if (Days.infected == true)
            {
                SceneManager.LoadScene(1);
            }
            if (Days.day >= 7)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    // method for spawning a new customer
    private void SpawnCustomer()
    {
        CustomerNum++;
        int ima = (int)UnityEngine.Random.Range(0, 5);
        if (responsibility < 3)
        {
            customerClone = Instantiate(noMaskNPC[ima], new Vector3(-1.63f, 1.21f, 0f), Quaternion.identity);
        }
        else
        {
            customerClone = Instantiate(maskNPC[ima], new Vector3(-1.63f, 1.21f, 0f), Quaternion.identity);
        }
        //customerClone = Instantiate(customer, new Vector3(-1.4f, .62f, 0f), Quaternion.identity);
    }
}
