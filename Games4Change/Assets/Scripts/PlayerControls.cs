using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    private int mood;
    private bool infected;
    public Text displayMood;
    // Start is called before the first frame update
    void Start()
    {
        mood = 10;
        infected = false;
    }

    // Update is called once per frame
    void Update()
    {
        displayMood.text = "Mood: " + mood;
    }

    public void setMood(int moodChange)
    {
        mood += moodChange;
        if (mood > 10)
        {
            mood = 10;
        }
        if (mood < 0)
        {
            mood = 0;
        }
    }

    public void Infected()
    {
        infected = true;
    }

    public int getMood()
    {
        return mood;
    }

    public bool getInfected()
    {
        return infected;
    }
}
