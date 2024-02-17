using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerCreation : MonoBehaviour
    {
    private int[,] map;
    private int width;
    private int height;
    private List<Vector2> positions = new List<Vector2>();
    [SerializeField] private GameObject spawner;
    public GameObject enemy;

    public void CreateEnemySpawners(int spawnerNumber)
    {
        map = GetComponent<CellularAutomata>().GetMap();
        width = CellularAutomata.width;
        height = CellularAutomata.height;

        getPotentialPositions();
        for (int i = 0; i < spawnerNumber; i++)
        {
            GameObject spawnerInstance = Instantiate(spawner);
            int num = Random.Range(0, positions.Count);
            spawnerInstance.transform.position = positions[num];
            spawner.GetComponent<EnemySpawner>().enemy = enemy;
            positions.RemoveAt(num);
        }
    }

    void getPotentialPositions()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (map[i, j] == 0)
                {
                    if (i == width - 1 || j == height - 1)
                    {
                        positions.Add(new Vector2(i, j));
                    }
                    else if (map[i, j + 1] == 1 || map[i, j - 1] == 1 || map[i + 1, j] == 1 || map[i - 1, j] == 1)
                    {
                        positions.Add(new Vector2(i, j));
                    }
                }
            }
        }
    }
}