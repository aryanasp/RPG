using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image _content;

    [SerializeField]
    private float barLerpSpeed;

    private float _currentFill;
    
    private float _maxValue;
    public float BarMaxValue
    {
        get => _maxValue;
        set
        {
            if (value < 0)
            {
                _maxValue = 0;
            }
            else
            {
                _maxValue = value;    
            }
        }
    }
    
    private float _currentValue;

    public float BarCurrentValue
    {
        get => _currentValue;
        set
        {
            if (value > BarMaxValue)
            {
                _currentValue = BarMaxValue;
            }
            else if (value < 0)
            {
                _currentValue = 0;
            }
            else
            {
                _currentValue = value;    
            }

            _currentFill = _currentValue / _maxValue;
        }
        
    }

    public void Initialize(float currentValue, float maxValue)
    {

        BarMaxValue = maxValue;
        BarCurrentValue = currentValue;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _content = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCurrentHealthChange();
    }

    private void CheckIfCurrentHealthChange()
    {
        if (Math.Abs(_currentFill - _content.fillAmount) > 0.0001f)
        {
            _content.fillAmount = Mathf.Lerp(_content.fillAmount, _currentFill, barLerpSpeed * Time.deltaTime);
        }
    }
}