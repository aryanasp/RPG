using System;

namespace Model
{
    public abstract class StatModel
    {
        protected string StatBarName;
        public string StatName => StatBarName;
        private float _statMaxValue;
        public float StatMaxValue
        {
            get => _statMaxValue;
            set
            {
                if (value < 0)
                {
                    _statMaxValue = 0;
                }
                else
                {
                    _statMaxValue = value;
                }
            }
        }
        private float _statCurrentValue;
        public float StatCurrentValue
        {
            get => _statCurrentValue;
            set
            {
                if (value > StatMaxValue)
                {
                    _statCurrentValue = StatMaxValue;
                }
                else if (value < 0)
                {
                    _statCurrentValue = 0;
                }
                else
                {
                    _statCurrentValue = value;
                }
            }
        }
        public virtual event Action<float, float> StatUpdater;
        public virtual void Enter(float currentStatValue, float maxStatValue)
        {
            StatMaxValue = maxStatValue;
            StatCurrentValue = currentStatValue;
        }
        public virtual void ChangeStatValue(float amount)
        {
            StatCurrentValue += amount;
        }
        public virtual void UpdateUI()
        {
            StatUpdater?.Invoke(StatCurrentValue, StatMaxValue);
        }
        public virtual void Exit()
        {
            UpdateUI();
        }
    }
}
