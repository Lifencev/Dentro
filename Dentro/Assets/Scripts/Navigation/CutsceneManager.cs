using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private float changeTime;
    [SerializeField] private float startChangeTime;
    [SerializeField] private LocalizeStringEvent localizedStringEvent;
    [SerializeField] private LocalizedString angerText;
    [SerializeField] private LocalizedString lazinessText;
    [SerializeField] private LocalizedString fearText;

    private void Awake()
    {
        changeTime = startChangeTime;
        Dictionary<string, LocalizedString> levelTitle = new()
        {
            {"AngerLevel", angerText },
            {"LazinessLevel", lazinessText },
            {"FearLevel", fearText }
        };

        localizedStringEvent.StringReference = levelTitle[NavigateMenuScript.currentLevel];
    }

    private void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime < 0)
        {
            SceneManager.LoadScene(NavigateMenuScript.currentLevel);
        }
    }
}
