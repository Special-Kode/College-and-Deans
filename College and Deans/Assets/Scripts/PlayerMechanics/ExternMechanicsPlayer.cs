using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExternMechanicsPlayer : MonoBehaviour
{
    [Header("Death Logic")]
    public bool death;

    [Header("Time-Health Logic")]
    [SerializeField] private float m_CurrentHealth = 100; //m_CurrentHealth
    public int CurrentHealth { 
        get { return (int)m_CurrentHealth; } 
        private set { } 
    }
    public int TimeLife= 120;
    [SerializeField] private int DamageAmount = 3; //TODO change this for attack damage

    public float TimeScaler;
    public int DamageScaler;
    public GameObject ResultsMenuUI;

    [Header("Invulnerability Logic")]
    public bool damage;
    private float NoDamageTimer;
    private bool canBeDamage;
    [SerializeField] private float Invulnerability = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        damage = false;
        m_CurrentHealth = TimeLife;
        NoDamageTimer = 0;
        canBeDamage = true;

        TimeScaler = 1;
        DamageScaler = 1;

        ResultsMenuUI = GameObject.Find("Results");
        ResultsMenuUI.SetActive(false);
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

        m_CurrentHealth -= (Time.deltaTime * TimeScaler);
        calculateHealth();
    }
    //se asigna la vida según la escala.x de la barra de vida,
    //con esto,si se hacen cambios de cuánto baja la barra de vida por cada golpe, se actualizará solo
    void calculateHealth()
    {
        if (m_CurrentHealth <= 0)
        {
            death = true;
            ResultsMenuUI.SetActive(true);
        }

    }
    void HandleDamage()
    {
        if (canBeDamage)
        {
            m_CurrentHealth -= DamageAmount;
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

    public void ScaleTime(float _scaleTime)
    {
        TimeScaler = _scaleTime;
    }

    //TODO receive float param
    public void ScaleDamage(int _scaleDamage)
    {
        DamageScaler = _scaleDamage;
    }
}
