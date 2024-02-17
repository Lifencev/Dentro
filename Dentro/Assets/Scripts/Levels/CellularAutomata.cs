using System;
using System.Collections.Generic;
using UnityEngine;

public class CellularAutomata : MonoBehaviour
{
    public const int width = 100;
    public const int height = 60;
    private int[,] map;
    [SerializeField] private string seed;
    [SerializeField] private int iterations;
    [SerializeField] private bool useRandomSeed;
    [Range(0, 100)]
    [SerializeField] private int randomFillPercent;
    [SerializeField] private GameObject Surf;
    [SerializeField] private GameObject Surf2;
    [SerializeField] private GameObject Map;
    [SerializeField] private GameObject Borders;
    GameObject[,] blocks = new GameObject[width, height];

    public int[,] GetMap()
    {
        return map;
    }

    public void DeleteBlock(int i, int j)
    {
        blocks[i, j] = null;
        map[i, j] = 0;
    }

    public GameObject[,] GetBlocks()
    {
        return blocks;
    }

    public void CreateMap()
    {
        Map = GameObject.FindGameObjectWithTag("Map");

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (map[i, j] == 1)
                {
                    Vector2 spawnPosition = new Vector2(i, j);
                    GameObject block = Instantiate(Surf, spawnPosition, Quaternion.identity);
                    block.transform.parent = Map.transform;
                    block.GetComponent<Surface1>().i = i;
                    block.GetComponent<Surface1>().j = j;
                    blocks[i, j] = block;
                }
            }
        }
    }

    public void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < iterations; i++)
        {
            smoothMap();
        }
    }

    public List<Coord> getMaxRoomRegion()
    {
        List<List<Coord>> regions = getRegions(0);

        List<Coord> max = regions[0];

        for (int i = 1; i < regions.Count; i++)
        {
            if (regions[i].Count > max.Count)
            {
                max = regions[i];
            }
        }

        return max;
    }

    List<List<Coord>> getRegions(int tileType)
    {
        List<List<Coord>> regions = new();
        int[,] mapFlags = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (mapFlags[x,y] == 0 && map[x,y] == tileType)
                {
                    List<Coord> newRegion = getRegionTiles(x, y);
                    regions.Add(newRegion);

                    foreach (Coord tile in newRegion)
                    {
                        mapFlags[tile.tileX, tile.tileY] = 1;
                    }
                }
            }
        }

        return regions;
    }

    List<Coord> getRegionTiles(int startX, int startY)
    {
        List<Coord> tiles = new List<Coord>();
        int[,] mapFlags = new int[width, height];
        int tileType = map[startX, startY];

        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(new Coord(startX, startY));
        mapFlags[startX, startY] = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();
            tiles.Add(tile);

            for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++)
            {
                for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++)
                {
                    if (isInMapRange(x,y) && (x == tile.tileX || y == tile.tileY))
                    {
                        if (mapFlags[x,y] == 0 && map[x,y] == tileType)
                        {
                            mapFlags[x, y] = 1;
                            queue.Enqueue(new Coord(x, y));
                        }
                    }
                }
            }
        }

        return tiles;
    }

    public static bool isInMapRange(float x, float y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Time.realtimeSinceStartup.ToString();
        }
        System.Random pseudoRandom = new System.Random(seed.GetHashCode());
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width-1 || y == 0)
                {
                    map[x, y] = 1;
                }
                else map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;

            }
        }
    }

    private void smoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighborCount = getSurroundingWallCount(x, y);

                if (neighborCount > 4)
                    map[x, y] = 1;
                else if (neighborCount < 4)
                    map[x, y] = 0;

            }
        }
    }

    private int getSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighborX = gridX - 1; neighborX <= gridX + 1; neighborX++)
        {
            for (int neighborY = gridY - 1; neighborY <= gridY + 1; neighborY++)
            {
                if (isInMapRange(neighborX, neighborY))
                {
                    if (neighborX != gridX || neighborY != gridY)
                    {
                        wallCount += map[neighborX, neighborY];
                    }
                }
                else
                {
                    wallCount++;
                }
                
            }
        }
        return wallCount;
    }
    public void spawnBorders()
    {
        Borders = GameObject.FindGameObjectWithTag("Borders");

        for(int i = 0; i < width; i++)
        {
            Vector2 spawn1 = new Vector2(i, -10);
            Vector2 spawn2 = new Vector2(i, height+10);
            GameObject border = Instantiate(Surf2, spawn1, Quaternion.identity);
            border.transform.parent = Borders.transform;
            border = Instantiate(Surf2, spawn2, Quaternion.identity);
            border.transform.parent = Borders.transform;
        }
        for (int j = -10; j <= height+5; j++)
        {
            Vector2 spawn1 = new Vector2(-1, j);
            Vector2 spawn2 = new Vector2(width, j);
            GameObject border = Instantiate(Surf2, spawn1, Quaternion.identity);
            border.transform.parent = Borders.transform;
            border = Instantiate(Surf2, spawn2, Quaternion.identity);
            border.transform.parent = Borders.transform;
        }
    }

    public struct Coord
    {
        public int tileX;
        public int tileY;

        public Coord(int x, int y)
        {
            tileX = x;
            tileY = y;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (map != null)
    //    {
    //        for (int x = 0; x < width; x++)
    //        {
    //            for (int y = 0; y < height; y++)
    //            {
    //                Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
    //                Vector3 pos = new Vector3(-width / 2 + x + 0.5f, -height / 2 + y + 0.5f, 0);
    //                Gizmos.DrawCube(pos, Vector3.one);
    //            }
    //        }
    //    }
    //}

}

    