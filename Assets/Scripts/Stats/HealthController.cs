using System;
using UnityEngine;

namespace Stats
{
    public class HealthController : IStatController
    {
        
        private Stat _healthStat;
        
        private float _initialHealthValue;
        private float _maxInitialHealthValue;


        public event Action<float, float> StatUpdater;

        public void Enter(float currentStatValue, float maxStatValue)
        {
            _healthStat = new Stat();
            _healthStat.Initialize("Health",currentStatValue, maxStatValue);
        }

        public string StatName()
        {
            return "Health";
        }

        public float StatCurrentValue()
        {
            return _healthStat.StatCurrentValue;
        }

        public float StatMaxValue()
        {
            return _healthStat.StatMaxValue;
        }


        public void IncreaseStatValue(float amount)
        {
            _healthStat.StatCurrentValue += amount;
        }

        public void DecreaseStatValue(float amount)
        {
            _healthStat.StatCurrentValue -= amount;
        }

        public void UpdateUI()
        {
            StatUpdater?.Invoke(_healthStat.StatCurrentValue, _healthStat.StatMaxValue);
        }


        // Update is called once per frame
        public void Exit()
        {
            UpdateUI();
        }
    
    }
}
