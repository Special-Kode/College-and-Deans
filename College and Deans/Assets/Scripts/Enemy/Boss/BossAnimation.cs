using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        enemy.EnemyAnimator.SetFloat("Horizontal", enemy.EnemyPathfinding.GetDirectionMov().x);
        enemy.EnemyAnimator.SetFloat("Vertical", enemy.EnemyPathfinding.GetDirectionMov().y);
        enemy.EnemyAnimator.SetFloat("Speed", enemy.EnemyPathfinding.GetDirectionMov().sqrMagnitude);
        enemy.EnemyAnimator.SetBool("IsJumping", enemy.BossIA.isJumping);
        enemy.EnemyAnimator.SetBool("Attack", enemy.BossIA.isAttacking);
    }
}
