using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.Localization;

public class PlayerUIUpdater : MonoBehaviour
{
    [SerializeField]private TMP_Text statsText;
    [SerializeField]private TMP_Text healthText;
    [SerializeField] private Stats playerStats;
    [SerializeField] private GameObject blackSquare;
    [SerializeField] private ChangeStatsText changeStats;

    public float fadeSpeed = 5;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_System>().currentStats;
        changeStats = FindObjectOfType<ChangeStatsText>();
    }

    void Update()
    {
        changeStats.UpdateStats();
    }

    public IEnumerator fade(bool fadeToBlack = true, float fadeSpeed = 5)
    {
        Color objectColor = blackSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + fadeSpeed * Time.deltaTime;

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - fadeSpeed * Time.deltaTime;

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}

