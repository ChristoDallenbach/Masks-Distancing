using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuStuff : MonoBehaviour
{
    public TextMeshProUGUI gameName;
    public Button startGame;
    // Start is called before the first frame update
    void Start()
    {
        gameName.text = "Name of Game";
        startGame.GetComponentInChildren<TextMeshProUGUI>().text = "Start Game";
        startGame.onClick.AddListener(start);
    }

    private void start()
    {
        SceneManager.LoadScene(0);
    }

}
