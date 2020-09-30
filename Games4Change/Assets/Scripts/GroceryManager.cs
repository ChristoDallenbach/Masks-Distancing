using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroceryManager : MonoBehaviour
{
    // array for the grocery list
    [SerializeField] Groceries[] groceries;
    // array for the money
    [SerializeField] Money[] money;
    private bool allClear;
    private NpcManager npcManager;
    //ensures money doesn't spawn upon the start and money goes in the register properly
    private bool canSpawnMoney;

    // Start is called before the first frame update
    void Start()
    {
        npcManager = GameObject.Find("GameManager").GetComponentInChildren<NpcManager>();
        canSpawnMoney = false;
        NewCustomerScan();
    }

    // Update is called once per frame
    void Update()
    {
        int check = 0;
        // check when the groceries is gone
        foreach (Groceries grocery in groceries)
        {
            if (grocery.Scanned == true)
            {
                check++;
            }

            if (check == 8)
            {
                allClear = true;
            }
        }

        // if all the groceries are bagged then go next
        if (allClear == true)
        {
            if (canSpawnMoney)
            {
                foreach (Money cash in money)
                {
                    cash.SpawnMoney();
                }
                canSpawnMoney = false;
            }
            npcManager.GroceriesBagged = true;
            //groceries appear once previous wave's money is put in
            //the only way to actually get that to work was to hardcode it
            if (money[0].InRegister && money[1].InRegister && money[2].InRegister&& 
                money[3].InRegister && money[4].InRegister && money[5].InRegister&&
                money[6].InRegister)
            {
                NewCustomerScan();
            }
        }
    }

    // Method to pick four grocieries to scan for the customer
    public void NewCustomerScan()
    {
        if (npcManager.SpawnGroceries)
        {
            int i = 0;

            foreach(Groceries grocery in groceries)
            {
                grocery.GetComponent<RectTransform>().anchoredPosition3D = grocery.StartPosition;
            }

            // loop through and pick 4 different items for scanning
            while (i < 4)
            {
                int randomNum = Random.Range(0, 7);

                // check if the item has beed chosen yet for the customer, if it has run again
                if (groceries[randomNum].Scanned != false)
                {
                    groceries[randomNum].Scanned = false;
                    groceries[randomNum].GetComponent<Image>().enabled = true;
                    i++;
                }
            }

            // reset the check for full bagged and the npc managers bool
            allClear = false;
            npcManager.GroceriesBagged = false;
            npcManager.SpawnGroceries = false;

            //allow money to spawn when groceies are clear after the 1st wave of groceries spawn
            canSpawnMoney = true;
        }
    }
}

