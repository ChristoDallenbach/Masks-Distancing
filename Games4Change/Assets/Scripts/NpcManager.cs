﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public GameObject customer;
    private GameObject customerClone;
    public GameObject dialog;
    private DialogManager dialogScript;
    private float tempTime = 0.0f;//This is just until scanning is done, don't knw how to check yet
    private float Timer = 0.0f;
    private float timeUntilC = 5.0f;
    private bool customerPresent;
    private int numCustomerInADay;// the number of costemers the player will see through the entire day
    private int CustomerNum;//the number of custemers the player has seen
    //these are npc stuff
    private int responsibility; //chances of wearing a mask
    private int kindness; //chances on being rude about puting the mask on

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
        CustomerTimer();
        if (dialogScript.DoneTalking)
        {
            tempTime += Time.deltaTime;
            if (tempTime > timeUntilC)
            {
                SetCustomerleave();
                tempTime = 0;
            }
        }
    }

    void CustomerTimer()
    {
        Timer += Time.deltaTime;
        if (Timer > timeUntilC && customerPresent == false)
        {
            customerPresent = true;
            Responsibility = (int)UnityEngine.Random.Range(0, 5);
            Kindness = (int)UnityEngine.Random.Range(0, 5);
            SpawnCustomer();
        }
    }

    public bool GetCustomer()
    {
        return customerPresent;
    }

    public void SetCustomerleave()
    {
        customerPresent = false;
        Timer = 0f;
        Destroy(customerClone);
        if(numCustomerInADay == CustomerNum)
        {
            Days.day += 1;
        }
    }

    private void SpawnCustomer()
    {
        CustomerNum++;
        customerClone = Instantiate(customer, new Vector3(-1.4f, .62f, 0f), Quaternion.identity);
    }
}
