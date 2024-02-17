using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private GameObject impactEffect;
    public GameObject player;
    private Rigidbody2D rb;
    public int force = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Physics.IgnoreLayerCollision(8, 9);

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Enemy") && !collision.collider.CompareTag("Boss"))
        {
            GameObject impact = Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(impact, impact.GetComponent<ParticleSystem>().main.duration);
            Destroy(gameObject);
        }

    }
}
