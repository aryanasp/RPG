using System;
using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;
using UnityEngine.UI;


public class StatUI : MonoBehaviour
{
    [SerializeField] private StatsHandler character;

    [SerializeField] private Image statBarImage;
    [SerializeField] private Text statBarText;
    private Dictionary<string, IStatController> _stats;

    void Awake()
    {
        _stats = character.Stats;
    }
    private void OnEnable()
    {
        _stats[gameObject.tag].StatUpdater += UpdateUI;
    }
    private void OnDisable()
    {
        _stats[gameObject.tag].StatUpdater -= UpdateUI;
    }

    private void UpdateUI(float currentValue, float maxValue)
    {
        statBarText.text = currentValue + "/" + maxValue;
        statBarImage.fillAmount = currentValue / maxValue;
    }
}