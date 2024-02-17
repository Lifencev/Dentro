using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AngryBoss : Boss
{
    [SerializeField] private GameObject poisonousBullet;

    private void Update()
    {
        if (currentStats.health <= 0.5f * currentStats.maxhealth)
        {
            GetComponent<EnemyShooting>().bullet = poisonousBullet;
        }
    }
}

