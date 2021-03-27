using System;

namespace Stats
{
    public interface IStatController
    {
        event Action<float, float> StatUpdater;

        void Enter(float currentStatValue, float maxStatValue);
        string StatName();
        float StatCurrentValue();
        float StatMaxValue();
        void IncreaseStatValue(float amount);
        void DecreaseStatValue(float amount);
        void UpdateUI();
        void Exit();
    }
}
