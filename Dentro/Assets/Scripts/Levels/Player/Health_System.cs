using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class Health_System : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI healthText;
    public HealthBar healthBarP;
    public HealthBar healthBarUI;
    private float lasthealed;

    public static Stats standardStats = new(50, 50, 10, 10, 10, 10);
    public Stats currentStats = new(50, 50, 10, 10, 10, 10);

    public Stats getstandardstats(){
        return standardStats;
    }
    public void setstandardstats(Stats a ){
        standardStats = a;
    }

    void Start()
    {
        healthBarP = GameObject.FindGameObjectWithTag("HealthBarLocal").GetComponent<HealthBar>();
        healthBarUI = GameObject.FindGameObjectWithTag("HealthBarUI").GetComponent<HealthBar>();
        healthText = GameObject.FindGameObjectWithTag("HealthBarText").GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        currentStats = standardStats;
        healthBarP.SetMaxHealth(currentStats.maxhealth);
        healthBarUI.SetMaxHealth(currentStats.maxhealth);
        lasthealed = 0;
    }

    void Update()
    {
        healthText.text = (int)currentStats.health + " / " + currentStats.maxhealth;
        if(Time.time > lasthealed + 1)
        {
            lasthealed = Time.time;
            if (currentStats.maxhealth - currentStats.health >= currentStats.regen/100)
            {
                currentStats.health += currentStats.health/100;
                healthBarP.SetHealth(currentStats.health);
                healthBarUI.SetHealth(currentStats.health);
            }
            else if (currentStats.health < currentStats.maxhealth)
            {
                currentStats.health = currentStats.maxhealth;
                healthBarP.SetHealth(currentStats.health);
                healthBarUI.SetHealth(currentStats.health);
            }
        }

        if (currentStats.health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("player death");
        Time.timeScale = 0;
        gameManager.gameOver();
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("EnemyBullet"))
        {
            TakeDamage(AEnemy.standardStats.damage);
        }

        else if (collision.collider.CompareTag("SurfaceBullet"))
        {
            TakeDamage(Boss.standardStats.damage);
        }

        else if (collision.collider.CompareTag("LazinessBullet"))
        {
            GetComponent<Animator>().SetBool("isLazyDirty", true);
            currentStats.speed = currentStats.speed > 5 ? currentStats.speed - 1 : currentStats.speed - 0.5f;
            TakeDamage(Boss.standardStats.damage);
        }
    }
    
    public void TakeDamage(float damage)
    {
        currentStats.health -= damage;
        healthBarP.SetHealth(currentStats.health);
        healthBarUI.SetHealth(currentStats.health);
        FindObjectOfType<AudioManager>().Play("player_damage");
    }
}
