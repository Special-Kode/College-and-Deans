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
    private float ResistanceScaler = 1; //TODO rename this properly

    [Header("Invulnerability Logic")]
    public bool damage;
    private float NoDamageTimer;
    private bool canBeDamaged;
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

        TimeBar = GameObject.Find("TimeLifeBar").GetComponent<BarAnimationScript>();
        TimeBar.SetMaxHealth(TimeLife);
        NoDamageTimer = 0;
        canBeDamaged = true;
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
            canBeDamaged = false;
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
        if (canBeDamaged)
        {
            m_CurrentHealth -= DamageAmount / ResistanceScaler;
            FindObjectOfType<SFXManager>().hurtSFX();
        }    
    }
    void AddNoDamageTime()
    {
        NoDamageTimer += Time.deltaTime;
        if (NoDamageTimer >= Invulnerability)
        {
            damage = false;
            canBeDamaged = true;
            NoDamageTimer = 0;
        }
    }

    public float GetTimescale()
    {
        return TimeScaler;
    }

    public void ScaleTime(float _scaleTime)
    {
        TimeScaler *= _scaleTime;
    }

    public void SetTimescale(float _timescale)
    {
        TimeScaler = _timescale;
    }

    public float GetResistanceScaler()
    {
        return ResistanceScaler;
    }

    //TODO receive float param
    public void ScaleResistance(float _scaleResistance)
    {
        ResistanceScaler *= _scaleResistance;
    }

    public void SetResistanceScaler(float _resistanceScaler)
    {
        ResistanceScaler = _resistanceScaler;
    }
}
