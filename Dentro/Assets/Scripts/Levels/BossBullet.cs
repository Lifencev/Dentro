using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private GameObject impactEffect;
    public GameObject player;
    private Rigidbody2D rb;
    public int force = 5;

    void Awake()
    {
        force = Random.value > 0.3 ? 3 : 8;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        FindObjectOfType<AudioManager>().Play("boss_shot");

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Enemy")
        {
            GameObject impact = Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(impact, impact.GetComponent<ParticleSystem>().main.duration);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("boss_bullet_explosion");

        }

    }
}
