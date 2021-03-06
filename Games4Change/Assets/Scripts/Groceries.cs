﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Groceries : MonoBehaviour
{
    private bool scanned;
    private bool bagged;
    [SerializeField]
    private AudioSource soundManager;
    [SerializeField]
    private AudioClip beepSound;

    // the start position of the groceries
    private Vector3 startPosition;

    // getter and setter for scanned
    public bool Scanned
    {
        get { return scanned; }
        set { scanned = value; }
    }

    public bool Bagged 
    { 
        get { return bagged; }
        set { bagged = value; }
    }

    public Vector3 StartPosition
    {
        get { return startPosition; }
    }

    // Start is called before the first frame update
    void Start()
    {
        scanned = true;
        bagged = true;
        gameObject.GetComponent<Image>().enabled = false;
        startPosition = GetComponent<RectTransform>().anchoredPosition3D;
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //scan the grocery item
        if (other.name == "Scanner")
        {
            if (!scanned)
            {
                Debug.Log("Beep"); //replace with sound
                soundManager.PlayOneShot(beepSound);
                scanned = true;
            }
            else
            {
                Debug.Log("This item has already been scanned."); //replace with popup or something stating this
            }

        }
        //put in bag
        if (other.name == "Bag" && scanned == true && gameObject.GetComponent<Image>().enabled == true)
        {
            bagged = true;
            gameObject.GetComponent<Image>().enabled = false;
            GetComponent<RectTransform>().anchoredPosition3D = startPosition;
        }

    }
}

