using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] private int enemyInterval = 3;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private GameObject player;
    private bool active = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player != null && Vector3.Distance(player.transform.position, gameObject.transform.position) < 10 && !active)
        {
            StartCoroutine(spawnEnemy(enemyInterval, enemy, enemyCount));
        }
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy, int enemyCount)
    {
        int counter = 0;
        while (counter < enemyCount)
        {
            Debug.Log(counter + " - заспавнився ворог");
            counter++;
            active = true;
            yield return new WaitForSeconds(Random.Range(interval - 1, interval + 1));
            GameObject newEnemy = Instantiate(enemy, transform.position + new Vector3(Random.Range(1, 1), Random.Range(1, -1), 0), Quaternion.identity);
            // newEnemy.GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
            active = false;
        }

        counter = 0;
        while (counter < enemyCount)
        {
            Debug.Log(counter);
            counter++;
            active = true;
            yield return new WaitForSeconds(Random.Range(interval*10 - 1, interval*10 + 1));
            GameObject newEnemy = Instantiate(enemy, transform.position + new Vector3(Random.Range(1, 1), Random.Range(1, -1), 0), Quaternion.identity);
            newEnemy.GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
            active = false;
        }

    }
}