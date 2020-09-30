using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    // the start position of the money
    private Vector3 startPosition;
    private bool inRegister; //is it in the register
    public Vector3 StartPosition
    {
        get { return startPosition; }
    }
    public bool InRegister
    {
        get { return inRegister; }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().enabled = false;
        startPosition = GetComponent<RectTransform>().anchoredPosition3D;
        inRegister = true;
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
            gameObject.GetComponent<Image>().enabled = false;
            inRegister = true;
        }

    }
    public void SpawnMoney()
    {
        gameObject.GetComponent<Image>().enabled = true;
        GetComponent<RectTransform>().anchoredPosition3D = startPosition;
        inRegister = false;
    }
}
