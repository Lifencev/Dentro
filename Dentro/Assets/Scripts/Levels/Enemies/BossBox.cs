using UnityEngine;

public class BossBox : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health = 100;
    [SerializeField] private Stats playerStats;
    [SerializeField] private Sprite damagedBossBox;
    public GameObject boss;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_System>().currentStats;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        health -= playerStats.damage;

        if (health <= maxHealth * 0.5f)
            GetComponent<SpriteRenderer>().sprite = damagedBossBox;
        if (health <= 0)
        {
            Instantiate(boss, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }   
    }
}
