using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_System : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float lastshot;
    [SerializeField] private float firerate;
    [SerializeField] private Gun gun;
    [SerializeField] private Stats playerStats;

    void Start()
    {
        gun = GetComponentInChildren<Gun>();
        playerStats = GetComponent<Health_System>().currentStats;

        lastshot = 0;
        firerate =(float)5/playerStats.firerate;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time > lastshot + firerate)
        {
            gun.Shoot(gameObject.tag);
            lastshot = Time.time;
        }
    }
}
