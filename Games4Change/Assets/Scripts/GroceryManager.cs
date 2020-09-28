using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroceryManager : MonoBehaviour
{
    // array for the grocery list
    [SerializeField] Groceries[] groceries;
    private bool allClear;
    private NpcManager npcManager;

    // Start is called before the first frame update
    void Start()
    {
        npcManager = GameObject.Find("GameManager").GetComponentInChildren<NpcManager>();

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
            npcManager.GroceriesBagged = true;

            NewCustomerScan();
        }
    }

    // Method to pick four grocieries to scan for the customer
    public void NewCustomerScan()
    {
        if (npcManager.SpawnGroceries)
        {
            int i = 0;

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
        }
    }
}

