using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Stats standardStats = new(100, 100, 10, 10, 10, 8);
    public Stats currentStats = new(100, 100, 10, 10, 10, 10);

    [SerializeField] protected HealthBar healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] protected GameObject player;
    [SerializeField] protected Stats playerStats;

    protected GameManager gameManager;
    protected bool playsound = false;

    protected void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("BossHealthBar").GetComponent<HealthBar>();
        healthText = GameObject.FindGameObjectWithTag("BossHealthText").GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        playerStats = player.GetComponent<Health_System>().currentStats;
        healthBar.transform.localScale = new Vector3(0.01f, 0.01f, 0);
        healthBar.SetMaxHealth(currentStats.maxhealth);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Play("blob_damage");
            currentStats.health -= playerStats.damage;
            healthBar.SetHealth(currentStats.health);
            healthText.text = (int)currentStats.health + " / " + currentStats.maxhealth;

            if (currentStats.health <= 0)
                Die();
        }
    }

    private void Die()
    {
        healthBar.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(SpawnPortal(1));
        Destroy(gameObject, 1);
    }

    protected IEnumerator SpawnPortal(float deathAnimDuration)
    {
        yield return new WaitForSeconds(deathAnimDuration - 0.1f);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<PortalSpawner>().SpawnPortal(transform.position);
    }

    public void Play(string name)
    {
        FindObjectOfType<AudioManager>().Play(name, Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position));
    }
}
