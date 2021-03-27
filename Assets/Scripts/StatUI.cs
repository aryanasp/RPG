using System;
using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;
using UnityEngine.UI;


public class StatUI : MonoBehaviour
{
    [SerializeField] private Character character;

    [SerializeField] private Image statBarImage;
    [SerializeField] private Text statBarText;

    private void OnEnable()
    {
        character.Stats[gameObject.tag].StatUpdater += UpdateUI;
    }

    private void UpdateUI(float currentValue, float maxValue)
    {
        statBarText.text = currentValue + "/" + maxValue;
        statBarImage.fillAmount = currentValue / maxValue;
    }
}