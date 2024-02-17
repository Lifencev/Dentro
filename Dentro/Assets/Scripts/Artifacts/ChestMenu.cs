using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMenu : MonoBehaviour
{

    public GameObject ChestMenuUI;
    [SerializeField] private Stats playerStats;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_System>().currentStats;
    }

    public void ImproveSpeed()
	{
		FindObjectOfType<AudioManager>().Play("upgrade");
		playerStats.speed++;
		ChestMenuUI.SetActive(false);
		Time.timeScale = 1f;
	}
	public void ImproveFirerate()
	{
		FindObjectOfType<AudioManager>().Play("upgrade");
		playerStats.firerate++;
		ChestMenuUI.SetActive(false);
		Time.timeScale = 1f;
	}
	public void ImproveRegenaration()
	{
		FindObjectOfType<AudioManager>().Play("upgrade");
		playerStats.regen++;
		ChestMenuUI.SetActive(false);
		Time.timeScale = 1f;
	}
	public void ImproveDamage()
	{
		FindObjectOfType<AudioManager>().Play("upgrade");
		playerStats.damage++;
		ChestMenuUI.SetActive(false);
		Time.timeScale = 1f;
	}

}
