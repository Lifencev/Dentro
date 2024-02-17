using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AngryEnemy : AEnemy
{
    new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Play("blob_damage");
            if (currentStats.health <= 0)
                Play("blob_death");
        }
    }

}
