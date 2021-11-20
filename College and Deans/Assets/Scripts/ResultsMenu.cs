using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsMenu : MonoBehaviour
{
    public Text resultText;
    public Image resultDiploma;

    public Sprite winDiploma;

    public void LoadMenu()
    {
        FindObjectOfType<PauseMenu>().LoadMenu();
    }

    public void SetWinningResult()
    {
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;

        traduce();
        resultDiploma.sprite = winDiploma;
    }

    void traduce()
    {
        if (PlayerPrefs.GetString("language", "e") == "e")
        {
            resultText.text = "Bienvenido a la plantilla de McDolan";
        }
        else
        {
            resultText.text = "Welcome to the McDolan squad";
        }
    }
}
