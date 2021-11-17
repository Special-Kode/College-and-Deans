using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Enemy enemy {get; private set;}
    public EnemyPathfinding EnemyPathfinding {get; private set;}
    public Rigidbody2D EnemyRigidbody2D  {get; private set;}
    public Animator EnemyAnimator {get; private set;}

    public RoomBehaviour Room;

    [SerializeField] private int health;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        EnemyPathfinding = GetComponent<EnemyPathfinding>();
        EnemyRigidbody2D = GetComponent<Rigidbody2D>();
        EnemyAnimator = GetComponent<Animator>();
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
            Room.EnemyAmount -= 1;
            Destroy(this.gameObject);
        }
    }
}
