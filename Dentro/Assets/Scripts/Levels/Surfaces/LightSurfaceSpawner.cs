using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LightSurfaceSpawner: MonoBehaviour
{
    private int width;
    private int height;
    [SerializeField] private GameObject lightblock;

    public void SpawnLights(int lightnumber)
    {
        GameObject Map = GameObject.FindGameObjectWithTag("Map");

        width = CellularAutomata.width;
        height = CellularAutomata.height;
  	    for(int i = 0; i < lightnumber; i++)
        {
          	int x = Random.Range(0, width);
          	int y = Random.Range(0, height);
          	GameObject lightObject = Instantiate(lightblock, new Vector3(x,y,0), Quaternion.identity);
            lightObject.transform.parent = Map.transform;
  	    }
    }
}