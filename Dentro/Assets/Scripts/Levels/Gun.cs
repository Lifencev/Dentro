using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform firePoint;
    Vector2 direction;

    private void Start()
    {
        firePoint = transform.childCount > 0 ? transform.GetChild(0).transform : transform;
    }

    private void Update()
    {
        direction = (transform.localRotation * Vector2.right).normalized;
    }

    public void Shoot(string owner)
    {
        FindObjectOfType<AudioManager>().Play("player_shot");
        GameObject go = Instantiate(bullet.gameObject, firePoint.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.owner = owner;
        goBullet.direction = direction;
    }
}
//comment to fix merge conflicts 