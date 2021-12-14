using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Enemy enemy {get; private set;}
    public EnemyPathfinding EnemyPathfinding {get; private set;}
    public Rigidbody2D EnemyRigidbody2D  {get; private set;}
    public Animator EnemyAnimator {get; private set;}
    public BossIA BossIA { get; private set; }
    public Boss_2_IA Boss_2_IA { get; private set; }
    public Boss_2_Animation Boss_2_Animation { get; private set; }
    public EnemyAI EnemyAI { get; private set; }

    public RoomBehaviour Room;

    [SerializeField] private int health;

    [SerializeField] private float damageAnimTime;
    [SerializeField] private bool isBeingDamaged;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        damageAnimTime = 0.0f;

        EnemyPathfinding = GetComponent<EnemyPathfinding>();
        EnemyRigidbody2D = GetComponent<Rigidbody2D>();
        EnemyAnimator = GetComponent<Animator>();
        EnemyAI = GetComponent<EnemyAI>();
        BossIA = GetComponent<BossIA>();
        Boss_2_IA = GetComponent<Boss_2_IA>();
        Boss_2_Animation = GetComponent<Boss_2_Animation>();
    }

    private void Update()
    {
        if (isBeingDamaged)
        {
            damageAnimTime += Time.deltaTime;
            if (damageAnimTime >= 0.2f)
            {
                isBeingDamaged = false;
                damageAnimTime = 0.0f;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    public Vector2 GetPosition()
    {
        return EnemyRigidbody2D.position;
    }

    public void GetHit(int damage)
    {
        damageAnimTime = 0.0f;
        isBeingDamaged = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        if (damage <= 0)
            damage = 1;

        /**

        //TODO: Check minimum damage to enemies when collision management is fixed
        if (damage == 0)
        {
            Debug.LogError("Damage should not be 0");
            damage = 1;
        }
        //*/

        health -= damage;
        if(health <= 0)
        {
            if (this.tag == "Enemy")
            {
                Room.EnemyAmount -= 1;
                Destroy(this.gameObject);
            }
            else if (this.tag == "Boss")
            {
                Destroy(this.gameObject);
                if (FindObjectOfType<LevelLoader>() != null)
                    FindObjectOfType<LevelLoader>().LoadNextStage();
                else
                {
                    FindObjectOfType<GameManager>().ResetGame();
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
    }
}
