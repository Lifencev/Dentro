using UnityEngine;

public class LazinessBoss : Boss
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject spawner;

    [SerializeField] private GameObject bullet;

    private new void Start()
    {
        base.Start();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.GetComponent<Shake>().start = true;
    }

    public void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

    public void TakeDamage(float damage)
    {
        currentStats.health -= damage;
        healthBar.SetHealth(currentStats.health);
        FindObjectOfType<AudioManager>().Play("boss_damage");

        if (currentStats.health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<Animator>().SetBool("isDead", true);
        GameObject.FindGameObjectWithTag("BossHealthBar").transform.localScale = new Vector3(0, 0, 0);

        float deathAnimDuration = 2;
        Destroy(gameObject, deathAnimDuration);
        FindObjectOfType<AudioManager>().StopAll();
        FindObjectOfType<AudioManager>().Play("boss_death");

        player.GetComponent<Health_System>().currentStats.speed = Health_System.standardStats.speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(player.GetComponent<Health_System>().currentStats.damage);
        }
    }
}
