using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    private int mood;
    private bool infected;
    private string happiness;
    public Text displayMood;

    public int Mood
    {
        get { return mood; }
        set
        {
            mood += value;
            if (mood > 10)
            {
                mood = 10;
            }
            if (mood < 0)
            {
                mood = 0;
            }
        }
    }

    public bool Infected
    {
        get { return infected; }
        set { infected = value; }
    }

    private string Happiness
    {
        get { return happiness; }
        set
        {
            if (mood > 8)
            {
                happiness = "happy";
            }
            else if (mood > 6)
            {
                happiness = "neutral";
            }
            else if (mood > 4)
            {
                happiness = "unhappy";
            }
            else
            {
                happiness = "angry";
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mood = 10;
        infected = false;
    }

    // Update is called once per frame
    void Update()
    {
        displayMood.text = "Mood: " + mood + " " + happiness;
    }
}