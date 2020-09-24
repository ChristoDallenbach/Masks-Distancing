using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public GameObject customer;
    public GameObject dialog;
    private DialogManager dialogScript;
    private float tempTime = 0.0f;//This is just until scanning is done, don't knw how to check yet
    private float Timer = 0.0f;
    private float timeUntilC = 5.0f;
    private bool customerPresent;
    //these are npc stuff
    private int responsability; //chances of wearing a mask
    private int kindness; //chances on being rude about puting the mask on

    // Start is called before the first frame update
    void Start()
    {
        dialogScript = dialog.GetComponent<DialogManager>();
        customerPresent = false;
        responsability = 0;
        kindness = 0;
    }

    // Update is called once per frame
    void Update()
    {
        customerTimer();
        if (dialogScript.getSaidHello() == true)
        {
            tempTime += Time.deltaTime;
            if (tempTime > timeUntilC)
            {
                setCustomerleave();
                tempTime = 0;
            }
        }
    }

    void customerTimer()
    {
        Timer += Time.deltaTime;
        if (Timer > timeUntilC && customerPresent == false)
        {
            customerPresent = true;
            responsability = (int)UnityEngine.Random.Range(0, 5);
            kindness = (int)UnityEngine.Random.Range(0, 5);
            SpawnCustomer();
        }
    }

    public bool getCustomer()
    {
        return customerPresent;
    }

    public void setCustomerleave()
    {
        customerPresent = false;
        Timer = 0f;
    }

    private void SpawnCustomer()
    {
        Instantiate(customer, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public int getKindness()
    {
        return kindness;
    }

    public int getRespnse()
    {
        return responsability;
    }
}
