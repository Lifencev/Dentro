using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stats
{
    public float maxhealth = 1;
    public float health = 1;
    public float speed = 1;
    public float damage = 1;
    public float firerate = 1;
    public float regen = 1;

    public Stats(float health = 1, float maxhealth = 1, float speed = 1, float regen = 1, float firerate = 1, float damage = 1)
    {
        this.maxhealth = maxhealth;
        this.health = health;
        this.speed = speed;
        this.damage = damage;
        this.firerate = firerate;
        this.regen = regen;
    }

    public void ChangeValues(float health = 1, float maxhealth = 1, float speed = 1, float regen = 1, float firerate = 1, float damage = 1)
    {
        this.maxhealth = maxhealth;
        this.health = health;
        this.speed = speed;
        this.damage = damage;
        this.firerate = firerate;
        this.regen = regen;
    }

    public void Multiply(float num)
    {
        maxhealth *= num;
        health *= num;
        speed *= num;
        damage *= num;
        firerate *= num;
        regen *= num;
    }

}
