using System;
using UnityEngine;

namespace Stats
{
    public class ManaController : IStatController
    {
        private Stat _manaStat;
        
        private float _initialManaValue;
        private float _maxInitialManaValue;

        public event Action<float, float> StatUpdater;

        public void Enter(float currentStatValue, float maxStatValue)
        {
            _manaStat = new Stat();
            _manaStat.Initialize("Mana", currentStatValue, maxStatValue);
        }

        public string StatName()
        {
            return "Mana";
        }

        public float StatCurrentValue()
        {
            return _manaStat.StatCurrentValue;
        }

        public float StatMaxValue()
        {
            return _manaStat.StatMaxValue;
        }


        public void IncreaseStatValue(float amount)
        {
            _manaStat.StatCurrentValue += amount;
        }

        public void DecreaseStatValue(float amount)
        {
            _manaStat.StatCurrentValue -= amount;
        }

        public void UpdateUI()
        {
            StatUpdater?.Invoke(_manaStat.StatCurrentValue, _manaStat.StatMaxValue);
        }

        // Update is called once per frame
        public void Exit()
        {
            UpdateUI(); 
        }
    }
}
