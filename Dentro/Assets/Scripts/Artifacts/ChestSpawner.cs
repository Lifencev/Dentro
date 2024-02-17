using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ChestSpawner: MonoBehaviour
{
    private int width;
    private int height;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject Chests;

    public void SpawnChests(int chestNumber)
    {
        width = CellularAutomata.width;
        height = CellularAutomata.height;
  	    for(int i = 0; i < chestNumber; i++)
        {
          	int x = Random.Range(0, width);
          	int y = Random.Range(0, height);
          	GameObject chestObject = Instantiate(chest, new Vector3(x,y,0), Quaternion.identity);
            if (FindObjectOfType<GameManager>().automata.GetBlocks()[x, y] != null)
                Destroy(FindObjectOfType<GameManager>().automata.GetBlocks()[x, y]);
            chestObject.transform.parent = GameObject.FindGameObjectWithTag("Chests").transform;
  	    }
    }
}