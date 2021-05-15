using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class CharacterStatEventArgs : EventArgs
    {
        public GameObject Character;
        
        public ICharacterStat Stat;
        public float StatCurrentValue { set; get; }
        public float StatMaxValue { set; get; }
    }

    // Character stat mechanics interface
    
    public interface ICharacterStat
    {
        string Name { get; }
        
        float StatMaxValue { get; }
        
        float StatCurrentValue { get; }
        
        float StatValuePercentage { get; }

        void ChangeStatValue(float amount, bool isIncreasingly, bool isPercentile, bool isFromMaxValue);
    }
    
    // Character health mechanics
    public class CharacterHealth : ICharacterStat
    {
        public string Name => "Health";
        private float _statMaxValue;
        private float _statCurrentValue;

        // define StatStatMaxValue value setter && getter
        public float StatMaxValue
        {
            get => _statMaxValue;
            private set
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

        // define StatCurrentValue value setter && getter
        public float StatCurrentValue
        {
            get => _statCurrentValue;
            private set
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

        // define statValuePercentage value getter
        public float StatValuePercentage
        {
            get
            {
                if (StatMaxValue > 0)
                {
                    return StatCurrentValue / StatMaxValue;
                }

                return 0;
            }
        }
        
        public void ChangeStatValue(float amount, bool isIncreasingly, bool isPercentile, bool isFromMaxValue)
        {
            var changeValue = amount * (isIncreasingly ? 1 : -1);
            if (isPercentile)
            {
                var baseValue = isFromMaxValue ? StatMaxValue : StatCurrentValue;
                StatCurrentValue += baseValue * changeValue;
            }
            else
            {
                StatCurrentValue += changeValue;
            }
        }

        // Constructors
        // Constructor for identify
        
        // Constructor for initialize stat
        public CharacterHealth(float initialValue, float maxInitialValue)
        {
            StatMaxValue = maxInitialValue;
            StatCurrentValue = initialValue;
        }
    }
    
    // Character mana mechanics
    public class CharacterMana : ICharacterStat
    {
        public string Name => "Mana";
        private float _statMaxValue;
        private float _statCurrentValue;

        // define StatStatMaxValue value setter && getter
        public float StatMaxValue
        {
            get => _statMaxValue;
            private set
            {
                if (value <= 0)
                {
                    _statMaxValue = 0.1f;
                }
                else
                {
                    _statMaxValue = value;
                }
            }
        }

        // define StatCurrentValue value setter && getter
        public float StatCurrentValue
        {
            get => _statCurrentValue;
            private set
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

        // define statValuePercentage value getter
        public float StatValuePercentage
        {
            get
            {
                if (StatMaxValue > 0)
                {
                    return StatCurrentValue / StatMaxValue;
                }
                return 0;
            }
        }
        
        public void ChangeStatValue(float amount, bool isIncreasingly, bool isPercentile, bool isFromMaxValue)
        {
            var changeValue = amount * (isIncreasingly ? 1 : -1);
            if (isPercentile)
            {
                var baseValue = isFromMaxValue ? StatMaxValue : StatCurrentValue;
                StatCurrentValue += baseValue * changeValue;
            }
            else
            {
                StatCurrentValue += changeValue;
            }
        }
        
        // Constructors
        // Constructor for identify
        public CharacterMana()
        {
            StatMaxValue = 0;
            StatCurrentValue = 1;
        }
        // Constructor for initialize stat
        public CharacterMana(float initialValue, float maxInitialValue)
        {
            StatMaxValue = maxInitialValue;
            StatCurrentValue = initialValue;
        }
    }

    public interface ICharacterStatModel
    {
        GameObject Character { set; get; }
        
        event EventHandler<CharacterStatEventArgs> OnStatChanged;
        ICharacterStat Stat { set; get; }

        void Initialize(string key);
        void InitializeStats();
        void SwitchStat(string key);
        void ChangeStatValue(string statName, float amount, bool isIncreasingly, bool isPercentile,
            bool isFromMaxValue);
    }

    public class CharacterStatModel : ICharacterStatModel
    {
        private Dictionary<string, ICharacterStat> _stats;

        // ICharacterStatModel interface implemented
        public GameObject Character { get; set; }
        public event EventHandler<CharacterStatEventArgs> OnStatChanged = (sender, e) => { };
        public ICharacterStat Stat { get; set; }

        // Constructor initializes stats initial values
        public CharacterStatModel(float[] healthSetting, float[] manaSetting)
        {
            _stats = new Dictionary<string, ICharacterStat>();
            var initialHealthStat = new CharacterHealth(healthSetting[0], healthSetting[1]);
            _stats[initialHealthStat.Name] = initialHealthStat;
            var initialManaStat = new CharacterMana(manaSetting[0], manaSetting[1]);
            _stats[initialManaStat.Name] = initialManaStat;
            Stat = initialHealthStat;
        }

        public void Initialize(string key)
        {
            Stat = _stats[key];
            var eventArgs = new CharacterStatEventArgs
            {
                Stat = Stat
            };
            OnStatChanged(this, eventArgs);
        }

        public void InitializeStats()
        {
            foreach (var statsKey in _stats.Keys)
            {
                Initialize(statsKey);
            }
        }

        public void SwitchStat(string key)
        {
            Stat = _stats[key];
        }

        public void ChangeStatValue(string statName, float amount, bool isIncreasingly, bool isPercentile,
            bool isFromMaxValue)
        {
            Stat = _stats[statName];
            Stat.ChangeStatValue(amount, isIncreasingly, isPercentile, isFromMaxValue);
            var eventArgs = new CharacterStatEventArgs
            {
                Character = Character, Stat = Stat, StatMaxValue = Stat.StatMaxValue, StatCurrentValue = Stat.StatCurrentValue
            };
            OnStatChanged(this, eventArgs);
        }
    }
}