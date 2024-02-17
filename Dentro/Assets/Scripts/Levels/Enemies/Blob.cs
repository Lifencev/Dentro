using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Blob : AEnemy
{
    [SerializeField] private float timer;
    [SerializeField] private float startTimerValue;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask whatToHit;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = startTimerValue;
            Attack();
        }
    }

    new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D (collision);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Play("blob_damage");
            if (currentStats.health <= 0)
                Play("blob_death");
        }
    }

    void Attack()
    {
        Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange, whatToHit);

        for (int i = 0; i < objectsToDamage.Length; i++)
        {
            if (objectsToDamage[i].CompareTag("Player"))
            {
                objectsToDamage[i].GetComponent<Health_System>().TakeDamage(currentStats.damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
