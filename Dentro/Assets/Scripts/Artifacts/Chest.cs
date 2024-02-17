using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest: MonoBehaviour
{
    [SerializeField] private ChestMenu chestMenu;

    private void Start()
    {
	    chestMenu = GameObject.FindGameObjectWithTag("ChestCanvas").GetComponent<ChestMenu>();
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player")) 
	    {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("click");
	        Time.timeScale = 0f;
	        chestMenu.ChestMenuUI.SetActive(true);
        }
    }   
}