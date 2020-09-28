using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //remove the money once entering the register  
        if (other.name == "CashRegister")
        {
            Debug.Log("Cha-ching"); //replace with sound
            Destroy(this.gameObject);
        }

    }
}
