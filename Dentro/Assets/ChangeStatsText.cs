using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class ChangeStatsText : MonoBehaviour
{

    [SerializeField] private LocalizedString LocalString;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] public Stats playerStats;

    private float speed;
    private float firerate;
    private float regeneration;
    private float damage;

    private void Start()
    {
        statsText = GetComponent<TextMeshProUGUI>();
        playerStats = FindObjectOfType<Health_System>().currentStats;
        speed = playerStats.speed;
        firerate = playerStats.firerate;
        regeneration = playerStats.regen;
        damage = playerStats.damage;
    }

    private void OnEnable()
    {
        LocalString.Arguments = new object[] { speed, firerate, regeneration, damage };
        LocalString.StringChanged += UpdateText;
    }

    private void OnDisable()
    {
        LocalString.StringChanged -= UpdateText;
    }

    private void UpdateText(string value)
    {
        statsText.text = value;
    }

    public void UpdateStats()
    {
        speed = playerStats.speed;
        firerate = playerStats.firerate;
        regeneration = playerStats.regen;
        damage = playerStats.damage;

        LocalString.Arguments[0] = speed;
        LocalString.Arguments[1] = firerate;
        LocalString.Arguments[2] = regeneration;
        LocalString.Arguments[3] = damage;

        LocalString.RefreshString();
    }
}
