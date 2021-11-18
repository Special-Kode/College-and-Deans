using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Enemy enemy {get; private set;}
    public EnemyPathfinding EnemyPathfinding {get; private set;}
    public Rigidbody2D EnemyRigidbody2D  {get; private set;}
    public Animator EnemyAnimator {get; private set;}
    public BossIA BossIA { get; private set; }

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
        health -= damage;
        if(health <= 0)
        {
            if (this.tag == "Enemy")
            {
                Destroy(this.gameObject);
            }
            else if (this.tag == "Boss")
            {
                if (FindObjectOfType<LevelLoader>() != null)
                    FindObjectOfType<LevelLoader>().LoadNextLevel();
                else
                    Destroy(this.gameObject);
                    SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
