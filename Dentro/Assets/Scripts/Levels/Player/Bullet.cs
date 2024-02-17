using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20;
    [SerializeField] private GameObject impactEffect;
    public string owner;

    [SerializeField] private Vector2 velocity;

    public Vector2 direction = new(1,0);

    private void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;

        position += velocity * Time.fixedDeltaTime;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != owner)
        {
            GameObject impact = Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(impact, impact.GetComponent<ParticleSystem>().main.duration);
            Destroy(gameObject);
        }
        
    }
}
