using System;
using System.Collections.Generic;
using Controller;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class StatView : MonoBehaviour
    {
        [SerializeField]
        private StatController character;

        private StatController _lastCharacter;
        public GameObject Character
        {
            get => character.gameObject;
            set
            {
                if (value != null)
                {
                    if (value.GetComponent<StatController>() != null)
                    {
                        if (_stats != null)
                        {
                            OnDisable();
                        }
                        character = value.GetComponent<StatController>();
                        OnEnable();
                    } 
                }
            }
        }

        [SerializeField] private Image statBarImage;
        [SerializeField] private Text statBarText;
        private Dictionary<string, StatModel> _stats;
        [SerializeField] private float lerpSpeed;
        private bool _uiShouldUpdate = false;
        private float _currentValue;
        private float _maxValue;

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

        void Update()
        {
            
            if (Math.Abs(statBarImage.fillAmount - _currentValue / _maxValue) < 0.01f)
            {
                _uiShouldUpdate = false;
            }

            if (_uiShouldUpdate)
            {
                LerpStatBar();
            }
        }
    
        private void UpdateUI(float currentValue, float maxValue)
        {
            if (statBarText != null)
            {
                statBarText.text = currentValue + "/" + maxValue;
            }
            _uiShouldUpdate = true;
            _currentValue = currentValue;
            _maxValue = maxValue;
        }   

        private void LerpStatBar()
        {
            statBarImage.fillAmount = Mathf.Lerp(statBarImage.fillAmount , _currentValue / _maxValue, Time.deltaTime * lerpSpeed);
        }
    }
}