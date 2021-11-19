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
    public RoomBehaviour Room;

    [SerializeField] private int health;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        EnemyPathfinding = GetComponent<EnemyPathfinding>();
        EnemyRigidbody2D = GetComponent<Rigidbody2D>();
        EnemyAnimator = GetComponent<Animator>();
        BossIA = GetComponent<BossIA>();
    }

    public Vector2 GetPosition()
    {
        return EnemyRigidbody2D.position;
    }

    public void GetHit(int damage)
    {
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
