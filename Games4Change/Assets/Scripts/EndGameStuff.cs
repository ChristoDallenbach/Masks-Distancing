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
        backToMenu.GetComponentInChildren<TextMeshProUGUI>().text = "Main menu";
        backToMenu.onClick.AddListener(goBackToMain);
        if (Days.infected == true)
        {
            result.text = "You got infected";
        }
        else if(Days.infected == false && Days.day < 7)
        {
            result.text = "Your mental health got too low";
        }
        else
        {
            result.text = "you lasted a week";
        }
    }

    private void goBackToMain()
    {
        SceneManager.LoadScene(2);
    }
}
