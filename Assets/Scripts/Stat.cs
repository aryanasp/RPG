public class Stat
{
    private string _statName;

    public string StatName
    {
        get => _statName;
        set => _statName = value;
    }

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

    public void Initialize(string statName, float currentValue, float maxValue)
    {
        StatName = statName;
        StatMaxValue = maxValue;
        StatCurrentValue = currentValue;
    }
}