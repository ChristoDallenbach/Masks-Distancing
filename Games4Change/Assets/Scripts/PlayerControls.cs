using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum E_Mood { Happy = 0, Neutral = 1, Unhappy = 2, Angry = 3}

public class PlayerControls : MonoBehaviour
{
    private int mood;
    private E_Mood happiness;
    [SerializeField]
    private TextMeshProUGUI displayMood;
    [SerializeField]
    private Sprite[] moodImages;
    [SerializeField]
    private Image moodImage;

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
            UpdateUI();
        }
    }

    private E_Mood Happiness
    {
        get { return happiness; }
        set
        {
            if (mood > 8)
            {
                happiness = E_Mood.Happy;
            }
            else if (mood > 6)
            {
                happiness = E_Mood.Neutral;
            }
            else if (mood > 4)
            {
                happiness = E_Mood.Unhappy;
            }
            else
            {
                happiness = E_Mood.Angry;
            }
            UpdateUI();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mood = 10;
        Days.infected = false;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI() 
    {
        moodImage.sprite = moodImages[(int)happiness];
        displayMood.text = "Mood: " + Mood + " " + happiness;
    }
}