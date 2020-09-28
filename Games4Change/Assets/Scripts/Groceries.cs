using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groceries : MonoBehaviour
{
    private bool scanned;
    [SerializeField]
    private AudioSource soundManager;
    [SerializeField]
    private AudioClip beepSound;
    // Start is called before the first frame update
    void Start()
    {
        scanned = false;
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
        if (other.name == "Bag")
        {
            Destroy(this.gameObject);
        }

    }
}
