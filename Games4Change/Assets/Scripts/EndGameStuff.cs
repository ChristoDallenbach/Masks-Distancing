using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameStuff : MonoBehaviour
{
    public TextMeshProUGUI result;
    public Button backToMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (Days.infected == true)
        {
            result.text = "You got infected.";
        }
        else if(Days.day < 7)
        {
            result.text = "Your mental health got too low.";
        }
        else
        {
            result.text = "You lasted a week.";
        }
    }

    public void goBackToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
