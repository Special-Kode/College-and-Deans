using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    string language;
    private string languagePrefsName = "language";
    public Text interlude;

    private void Start()
    {
        if(interlude != null)
        {
            traduce();
        }        
    }

    private void Awake()
    {
        LoadData();
    }

    private void OnDestroy()
    {
        language = skill.idiom;
        SaveData();
    }

    void LoadData()
    {
        language = PlayerPrefs.GetString(languagePrefsName, "e");
        skill.idiom = language;
    }

    void SaveData()
    {
        PlayerPrefs.SetString(languagePrefsName, language);
    }

    void traduce()
    {
        if (language == "s")
        {
            interlude.text = "Pulsa para empezar";
        }
        else
        {
            interlude.text = "Click anywhere to start";
        }
    }
}
