using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExternMechanicsPlayer : MonoBehaviour
{
    [Header("Death Logic")]
    public bool death;

    [Header("Time-Health Logic")]
    [SerializeField] private float m_CurrentHealth = 100; //m_CurrentHealth
    public int CurrentHealth { 
        get { return (int)m_CurrentHealth; } 
    }
    public int TimeLife= 150;
    [SerializeField] private int DamageAmount = 3; //TODO change this for attack damage

    private float TimeScaler = 1;
    private int DamageScaler = 1;

    [Header("Invulnerability Logic")]
    public bool damage;
    private float NoDamageTimer;
    private bool canBeDamage;
    [SerializeField] private float Invulnerability = 1.0f;

    [Header("UI Elems")]
    public GameObject ResultsMenuUI;
    public Text resultText;
    public BarAnimationScript TimeBar;

    [Header("Other stuff")]
    public byte stopTimeOnStairs; 

    // Start is called before the first frame update
    void Start()
    {
        damage = false;
        m_CurrentHealth = TimeLife;
        TimeBar.SetMaxHealth(TimeLife);
        NoDamageTimer = 0;
        canBeDamage = true;

        resultText = GameObject.Find("Text").GetComponent<Text>();
        ResultsMenuUI = GameObject.Find("Results");
        ResultsMenuUI.SetActive(false);

        stopTimeOnStairs = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (damage)
        {
            HandleDamage();
            canBeDamage = false;
            AddNoDamageTime();
        }

        m_CurrentHealth -= (Time.deltaTime * TimeScaler * stopTimeOnStairs);
        CalculateHealth();
    }

    void CalculateHealth()
    {

        if (m_CurrentHealth <= 0)
        {
            death = true;
            if (PlayerPrefs.GetString("language", "e") == "e")
            {
                resultText.text = "YOU HAVE TO STUDY MORE";
            }
            else
            {
                resultText.text = "TIENES QUE ESTUDIAR MÁS";
            }
            ResultsMenuUI.SetActive(true);
            m_CurrentHealth = -0.1f;
        }

        TimeBar.SetHealth((int)(m_CurrentHealth + 1));
    }
    void HandleDamage()
    {
        if (canBeDamage)
        {
            m_CurrentHealth -= DamageAmount * DamageScaler;
            FindObjectOfType<SFXManager>().hurtSFX();
        }    
    }
    void AddNoDamageTime()
    {
        NoDamageTimer += Time.deltaTime;
        if (NoDamageTimer >= Invulnerability)
        {
            damage = false;
            canBeDamage = true;
            NoDamageTimer = 0;
        }
    }

    public float GetScaleTime()
    {
        return TimeScaler;
    }

    public void ScaleTime(float _scaleTime)
    {
        TimeScaler = _scaleTime;
    }

    public float GetScaleDamage()
    {
        return DamageScaler;
    }

    //TODO receive float param
    public void ScaleDamage(int _scaleDamage)
    {
        DamageScaler = _scaleDamage;
    }
}
