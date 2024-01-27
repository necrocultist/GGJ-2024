using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    private AttackEvent playerAttackEvent;
    private Player player;
    private int playerDamage;
    private float attackRange;
    private LayerMask enemyLayer;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerAttackEvent = GetComponent<AttackEvent>();
        attackRange = player.playerDetails.playerAttackRange;
        enemyLayer = player.playerDetails.enemyLayer;
        playerDamage = player.playerDetails.playerDamageAmount;
    }

    private void OnEnable()
    {
        playerAttackEvent.OnAttack += AttackEvent_OnAttack;
    }

    private void OnDisable()
    {
        playerAttackEvent.OnAttack -= AttackEvent_OnAttack;
    }
    
    private void AttackEvent_OnAttack(AttackEvent attackEvent, AttackEventArgs attackEventArgs)
    {
        Attack();
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Player Attack");
            if (enemy.GetComponent<Health>())
            {
                enemy.GetComponent<Health>().TakeDamage(playerDamage);
                //if (enemy.GetComponent<EnemyManager>().enemyAlive) knockbackManager.Knock(transform, enemy.transform, 0.01f, 55f);
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
