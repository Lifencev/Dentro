using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private PlayerUIUpdater uIUpdater;

    private void Start()
    {
        uIUpdater = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUIUpdater>();
        FindObjectOfType<AudioManager>().Play("teleport", Vector3.Distance(this.transform.position,GameObject.FindGameObjectWithTag("Player").transform.position));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            StartCoroutine(goToNextLevel());
    }

    IEnumerator goToNextLevel()
    {
        StartCoroutine(uIUpdater.fade());
        float fadingTime = 0;
        while (fadingTime < 1)
        {
            fadingTime += uIUpdater.fadeSpeed * Time.deltaTime;
            yield return null;
        }
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().loadNextLevel();
    }

    private void Update(){
        
        if (GameObject.FindGameObjectWithTag("Player") != null)
            FindObjectOfType<AudioManager>().SetVolume("teleport", Vector3.Distance(this.transform.position,GameObject.FindGameObjectWithTag("Player").transform.position));
    }
}
