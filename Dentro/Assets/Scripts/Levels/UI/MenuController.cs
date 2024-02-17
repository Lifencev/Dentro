using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject blackSquare;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject pauseMenuCanvas;
    public GameObject gameOverCanvas;

    private void Start()
    {
        playerUI.SetActive(true);
        pauseMenuCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        blackSquare.SetActive(false);
    }

    public void showPauseMenu()
    {
        pauseMenuCanvas.SetActive(true);
        playerUI.SetActive(false);
        Time.timeScale = 0;
    }

    public void continueGame()
    {
        pauseMenuCanvas.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1;
    }

    public void StartNewGame()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().StartNewGame();
    }

    public void showMainMenu()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}
