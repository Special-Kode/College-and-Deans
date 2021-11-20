using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Animation : MonoBehaviour
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
        enemy.EnemyAnimator.SetBool("Embestida", enemy.Boss_2_IA.embistiendo);
    }

    public void Bofetada()
    {
        enemy.EnemyAnimator.SetTrigger("Bofetada");
    }
}
