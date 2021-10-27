using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Enemy enemy {get; private set;}
    public EnemyPathfinding EnemyPathfinding {get; private set;}
    public Rigidbody2D EnemyRigidbody2D  {get; private set;}

    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        EnemyPathfinding = GetComponent<EnemyPathfinding>();
        EnemyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public Vector2 GetPosition()
    {
        return EnemyRigidbody2D.position;
    }
}
