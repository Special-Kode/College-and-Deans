using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    public float SpeedStat = 1;
    public float DamageStat = 1;
    public float TimeScaleStat = 1;
    public float BerserkStats = 1;

    public int modifier = 0;

    private void Awake()
    {
        if (FindObjectsOfType<StatsManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainMenu")
        {
            Destroy(gameObject);
        }
        if (scene.name == "SampleScene" && FindObjectOfType<ExternMechanicsPlayer>() != null)
        {
            SetStats();
        }
    }

    void SetStats()
    {
        //Speed stats
        FindObjectOfType<Movement>().SetSpeedMultiplier(SpeedStat);

        //Damage stats
        if(FindObjectOfType<AttackBehaviour>().getWeapon() != null)
            FindObjectOfType<AttackBehaviour>().getWeapon().SetDamageMultiplier((int)DamageStat);

        //TimeScale stats
        FindObjectOfType<ExternMechanicsPlayer>().ScaleTime(TimeScaleStat);

        //Berserk stats
        FindObjectOfType<ExternMechanicsPlayer>().ScaleDamage((int)DamageStat);


        //Update modifier
        
    }
}
