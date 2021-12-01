using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public Button PauseButton;
    public GameObject PauseMenuUI;
    public Text title, music;

    private void Start()
    {
        PauseButton.onClick.AddListener(Action);
        traduce();
    }

    public void Action()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        PauseButton.gameObject.SetActive(true);
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseButton.gameObject.SetActive(false);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<GameManager>().ResetGame();
        SceneManager.LoadScene("MainMenu");
    }

    void traduce()
    {
        if (PlayerPrefs.GetString("language", "e") == "e")
        {
            title.text = "Pause";
            music.text = "Music";
        }
        else
        {
            title.text = "Pausa";
            music.text = "Música";

        }
    }
}