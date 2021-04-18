using System;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CharacterStatChangedEventArgs : EventArgs
    {
        // Stat and change details
        public string StatName { get; set; }
        
        public bool IsIncreasingly { get; set; }
        public bool IsPercentile { set; get; }
    
        private bool _isFromMaxValue = false;
        public bool IsFromMaxValue
        {
            get => _isFromMaxValue;
            set
            {
                if (!IsPercentile)
                {
                    _isFromMaxValue = false;
                }
    
                _isFromMaxValue = value;
            }
        }
    
        private float _changeAmount = 0;
    
        public float ChangeAmount
        {
            get => _changeAmount;
            set => _changeAmount = Mathf.Abs(value);
        }
    }

    public class CharacterStatInitializedEventArgs : EventArgs
    {
        public string StatName;
    }

    public interface ICharacterStatView
    {
        string StatName {get; }

        event EventHandler<CharacterStatInitializedEventArgs> OnStatInitialized;
        event EventHandler<CharacterStatChangedEventArgs> OnStatChanged;
        
        Dictionary<string, float>  StatConfigs { set; get; }
    }
    
    public class CharacterStatView : MonoBehaviour, ICharacterStatView
    {
        // Config
        [SerializeField] private Image statBarImage;
        
        [SerializeField] private Text statBarText;
        
        //TODO should go to model
        [SerializeField] private float lerpSpeed;
        
        // Implement interface
        public string StatName => gameObject.tag;
        public event EventHandler<CharacterStatInitializedEventArgs> OnStatInitialized = (sender, e) => { };
        public event EventHandler<CharacterStatChangedEventArgs> OnStatChanged = (sender, e) => { };

        private readonly Dictionary<string, float> _statConfigs = new Dictionary<string, float>() {{"current", 0}, {"max", 1}};
        private bool _uiShouldUpdate;
        
        
        public Dictionary<string, float>  StatConfigs
        {
            get => _statConfigs;
            set
            {
                _statConfigs["max"] = value["max"];
                _statConfigs["current"] = value["current"];
                UpdateStatConf(_statConfigs["current"], _statConfigs["max"]);
            }
        }
        
        private void UpdateStatConf(float current, float max)
        {
            _uiShouldUpdate = true;
            
            if (statBarText != null)
            {
                statBarText.text = current + "/" + max;
            }
        }
        
        private void Start()
        {
            var eventArgs = new CharacterStatInitializedEventArgs
            {
                StatName = gameObject.tag
            };
            OnStatInitialized(this, eventArgs);
        }
        
        
        private void Update()
        {
            
            HandleStatBar();

            // Debug && Test
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (gameObject.CompareTag("Health"))
                {
                    var eventArgs = new CharacterStatChangedEventArgs
                    {
                        ChangeAmount = 10, IsIncreasingly = false, IsPercentile = false, StatName = "Health"
                    };
                    OnStatChanged(this, eventArgs);
                }
            
            }
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (gameObject.CompareTag("Mana"))
                {
                    var eventArgs = new CharacterStatChangedEventArgs
                    {
                        ChangeAmount = 4, IsIncreasingly = false, IsPercentile = false, StatName = "Mana"
                    };
                    OnStatChanged(this, eventArgs);
                }
            }
        }

        private void HandleStatBar()
        {
            if (Math.Abs(statBarImage.fillAmount - _statConfigs["current"] / _statConfigs["max"]) < 0.01f)
            {
                _uiShouldUpdate = false;
            }

            if (_uiShouldUpdate)
            {
                LerpStatBar();
            }
        }
        
        private void LerpStatBar()
        {
            //TODO should go to model
            statBarImage.fillAmount = Mathf.Lerp(statBarImage.fillAmount , _statConfigs["current"] / _statConfigs["max"], Time.deltaTime * lerpSpeed);
        }

    }
}