using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALevel : MonoBehaviour
{
    public GameObject Surf;
    public GameObject Surf2;
    public GameObject Player;
    public CellularAutomata automata;

    public void generateBlocks()
    {
        automata = GetComponent<CellularAutomata>();
        automata.GenerateMap();
        automata.CreateMap();
        automata.spawnBorders();
    }

}

