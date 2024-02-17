using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health = 100;
    [SerializeField] private Stats playerStats;
    [SerializeField] private Sprite damagedBossSpawner;

    private void Start()
    {
        playerStats = FindObjectOfType<Health_System>().currentStats;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            TakeDamage(playerStats.damage);
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= maxHealth * 0.5f)
            GetComponent<SpriteRenderer>().sprite = damagedBossSpawner;
        if (health <= 0)
        {
            Destroy(gameObject);
        }   
    }
}
