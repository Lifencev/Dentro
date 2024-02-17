using System.Threading;
using UnityEngine;

public class FearEnemy : AEnemy
{
    public float damage;
    [SerializeField] private RayAttack rayAttack;
    [SerializeField] private GameObject player;
    private bool playsound = false;
    private float timer = 2;

    private new void Start()
    {
        base.Start();
        rayAttack = GetComponent<RayAttack>();
        player = GameObject.FindGameObjectWithTag("Player");
        damage = currentStats.damage;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (Vector3.Distance(player.transform.position, transform.position) < 10 && timer < 0)
        {
            timer = 2;
            StartCoroutine(rayAttack.Attack(currentStats.damage));
            if (!playsound){
                Play("fear_attack");
                playsound = true;
            }
        }
        if (Vector3.Distance(player.transform.position, transform.position) >= 10)
        {
            timer = 2;
            playsound = false;
        }
        if (currentStats.health <= 0)
        {
            Play("fear_death");
            Destroy(gameObject);
        }
    }

    new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Play("fear_damage");
        }
    }
}
