using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;

    [SerializeField] private float timer = 0.9f;
    private readonly float startTimerValue = 0.9f;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = startTimerValue;
            Shoot();
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

}
