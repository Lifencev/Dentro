using System.Collections.Generic;
using UnityEngine;

public class BossBoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossBox;
    public GameObject boss;

    void Start()
    {

        List<CellularAutomata.Coord> maxRoomRegion = GetComponent<CellularAutomata>().getMaxRoomRegion();

        int maxHeight = maxRoomRegion[0].tileY;
        int minHeight = maxRoomRegion[0].tileY;
        for (int i = 1; i < maxRoomRegion.Count; i++)
        {
            if (maxRoomRegion[i].tileY > maxHeight)
                maxHeight = maxRoomRegion[i].tileY;

            else if (maxRoomRegion[i].tileY < minHeight)
                minHeight = maxRoomRegion[i].tileY;
        }

        
        int y = minHeight + (maxHeight-minHeight) * Random.Range(0, 20) / 100;

        List<int> xPositions = new List<int>();
        for (int i = 0; i < maxRoomRegion.Count; i++)
        {
            if (y == maxRoomRegion[i].tileY)
                xPositions.Add(maxRoomRegion[i].tileX);
        }
        int x = xPositions[Random.Range(0, xPositions.Count)];

        GameObject bossBoxObject = Instantiate(bossBox, new Vector3(x, y, 0), Quaternion.identity);
        bossBoxObject.GetComponent<BossBox>().boss = boss;
    }
    
}
