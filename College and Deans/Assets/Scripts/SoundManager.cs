using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    float sfxVol;
    float musicVol;
    private string sfxPrefsName = "sfx";
    private string musicPrefsName = "music";
    public Slider sfxSlider, musicSlider;

    private void Awake()
    {
        LoadData();
    }

    private void OnDestroy()
    {
        sfxVol = sfxSlider.value;
        musicVol = musicSlider.value;
        SaveData();
    }

    void LoadData()
    {
        sfxVol = PlayerPrefs.GetFloat(sfxPrefsName, 0);
        musicVol = PlayerPrefs.GetFloat(musicPrefsName, 0);
        sfxSlider.value = sfxVol;
        musicSlider.value = musicVol;
    }

    void SaveData()
    {
        PlayerPrefs.SetFloat(sfxPrefsName, sfxVol);
        PlayerPrefs.SetFloat(musicPrefsName, musicVol);
    }
}
