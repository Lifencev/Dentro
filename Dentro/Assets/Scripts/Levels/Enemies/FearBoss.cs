using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FearBoss : Boss
{
    [SerializeField] private RayAttack rayAttack;
    [SerializeField] private Transform shootPoint;
    public int damage = 3;
    private float timer = 2;

    private float teleportTimer = 3;

    private new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        rayAttack = GetComponent<RayAttack>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (Vector3.Distance(player.transform.position, transform.position) < 10 && timer < 0)
        {
            timer = 2;
            StartCoroutine(rayAttack.Attack(currentStats.damage));
            if (!playsound)
            {
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

        teleportTimer -= Time.deltaTime;
        if (teleportTimer < 0)
        {
            if (Random.value > 0.2)
                TeleportAroundPlayer();
            else Teleport();
            teleportTimer = 3;
        }
    }

    private void Teleport()
    {
        int rangeX;
        int rangeY;
        if (CellularAutomata.width - transform.position.x < 4)
        {
            rangeX = (int)(CellularAutomata.width - transform.position.x);
        }
        else rangeX = 4;
        if (CellularAutomata.height - transform.position.y < 4)
        {
            rangeY = (int)(CellularAutomata.height - transform.position.y);
        }
        else rangeY = 4;

        Vector3 newPosition = transform.position + new Vector3(Random.Range(0, rangeX), Random.Range(0, rangeY), 0);

        transform.position = newPosition;
    }

    private void TeleportAroundPlayer()
    {
        Vector3 playerPos = player.transform.position;
        float deltaX = Mathf.Abs(playerPos.x - transform.position.x);
        float deltaY = Mathf.Abs(playerPos.y - transform.position.y);

        List<Vector3> potentialPositions = new();

        for (float x = playerPos.x - deltaX; x <= playerPos.x + deltaX; x += deltaX)
        {
            for (float y = playerPos.y - deltaY; y <= playerPos.y + deltaY; y += deltaY)
            {
                if ((x != transform.position.x || y != transform.position.y) && CellularAutomata.isInMapRange(x, y)
                    && gameManager.automata.GetMap()[(int)x, (int)y] == 0 && (x != playerPos.x || y != playerPos.y))
                {
                    potentialPositions.Add(new Vector3(x, y, transform.position.z));
                }
            }
        }
        if (potentialPositions.Count > 0)
            transform.position = potentialPositions[Random.Range(0, potentialPositions.Count)];
    }
}
