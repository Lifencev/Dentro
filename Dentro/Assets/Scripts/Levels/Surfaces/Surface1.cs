using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class Surface1 : ASurface
{
    public Animator animator;
    public UnityEngine.Rendering.Universal.Light2D surfaceLight;
    private GameManager gameManager;
    bool isPoisonous = false;
    readonly int damage = 5;
    public int i;
    public int j;


    void Start()
    {
        animator.SetBool("destruct", false);
        surfaceLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            surfaceLight.intensity = 5f;
            animator.SetBool("destruct", true);
            Destroy(gameObject, 0.2f);
            Play("surf_destroy");
            UpdateMap.MapUpdate();
            gameManager.automata.DeleteBlock(i, j);
        }
        
        if (collision.gameObject.CompareTag("SurfaceBullet"))
        {
            BecomePoisonous();
            MakeOtherPoisonous();
        }

        if (isPoisonous && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health_System>().TakeDamage(damage);
        }
    }

    public void BecomePoisonous()
    {
        GetComponent<Animator>().SetBool("isPoisonous", true);
        surfaceLight.intensity = 5f;
        isPoisonous = true;
    }

    void MakeOtherPoisonous()
    {
        List<Surface1> neighbours = GetNeighbourSurfBlocks();
        foreach (Surface1 block in neighbours)
        {
            block.BecomePoisonous();
        }
    }

    List<Surface1> GetNeighbourSurfBlocks()
    {
        List<Surface1> neighbours = new List<Surface1>();

        for (int x = i - 1; x <= i + 1; x++)
        {
            for (int y = j - 1; y <= j + 1; y++)
            {
                if (CellularAutomata.isInMapRange(x,y) && gameManager.automata.GetMap()[x,y] == 1 && IsOnSurface(x,y))
                {
                    neighbours.Add(gameManager.automata.GetBlocks()[x,y].GetComponent<Surface1>());
                }
            }
        }

        return neighbours;
    }

    bool IsOnSurface(int i, int j)
    {
        for (int x = i - 1; x <= i + 1; x++)
        {
            for (int y = j - 1; y <= j + 1; y++)
            {
                if (!CellularAutomata.isInMapRange(x, y) || gameManager.automata.GetMap()[x, y] == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
