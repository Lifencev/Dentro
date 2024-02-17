using UnityEngine;
using Pathfinding;

public class AEnemy : MonoBehaviour
{
    [SerializeField] private AIPath aIPath;
    [SerializeField] protected Stats playerStats;
    [SerializeField] protected HealthBar healthBar;

    public bool isWithLocalHealthBar = true;

    public static Stats standardStats = new(25, 25, 10, 10, 10, 1);
    public Stats currentStats = new(25, 25, 10, 10, 10, 1);

    protected void Start()
    {
        aIPath = GetComponent<AIPath>();
        GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_System>().currentStats;
        if (isWithLocalHealthBar)
        {
            healthBar = GetComponentInChildren<HealthBar>();
            healthBar.SetMaxHealth(currentStats.maxhealth);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(playerStats.damage);

            if (currentStats.health < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    protected void TakeDamage(float damage)
    {
        currentStats.health -= damage;
        if (isWithLocalHealthBar)
            healthBar.SetHealth(currentStats.health);
    }

    public void Play(string name)
    {
        FindObjectOfType<AudioManager>().Play(name, Vector3.Distance(this.transform.position,GameObject.FindGameObjectWithTag("Player").transform.position));
    }
}
