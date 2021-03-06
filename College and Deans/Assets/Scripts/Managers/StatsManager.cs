using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    public float SpeedStat = 1;
    public float DamageStat = 1;
    public float TimeScaleStat = 1;
    public float ResistanceStat = 1;

    public float SeeFullMinimap = 0.55f;

    [Header("Modifier")]
    public int modifier = 0;

    [Header("UI Persistancy")]
    public Sprite modSprite;
    public Sprite[] enhSprites = new Sprite[10];

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
        //Update modifier
        FindObjectOfType<AnimatorPlayerScript>().InitWeapon(modifier);

        //Speed stats
        FindObjectOfType<Movement>().SetSpeedMultiplier(SpeedStat);

        //Damage stats
        if(FindObjectOfType<AttackBehaviour>().getWeapon() != null)
            FindObjectOfType<AttackBehaviour>().getWeapon().SetDamageMultiplier(DamageStat);

        //TimeScale stats
        FindObjectOfType<ExternMechanicsPlayer>().SetTimescale(TimeScaleStat);

        //Berserk stats
        FindObjectOfType<ExternMechanicsPlayer>().SetResistanceScaler(ResistanceStat);
    }
}
