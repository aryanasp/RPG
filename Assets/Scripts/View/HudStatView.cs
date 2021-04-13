using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class HudStatBarInitializedEventArgs : EventArgs
    {
        public string Key;
    }
    
    public interface IHudStatView
    {
        string StatKey {get; }
        GameObject Character { get; set; }
        Dictionary<string, float>  StatConfigs { set; get; }
        event EventHandler<HudStatBarInitializedEventArgs> OnStatInitialized;
    }
    
    public class HudStatView : MonoBehaviour, IHudStatView
    {
        [SerializeField] private Image statBarImage;
        [SerializeField] private Text statBarText;
        
        public string StatKey => gameObject.tag;
        public event EventHandler<HudStatBarInitializedEventArgs> OnStatInitialized = (sender, e) => { };
        public GameObject Character { set; get; }
        
        private readonly Dictionary<string, float> _statConfigs = new Dictionary<string, float>() {{"current", 0}, {"max", 1}};
        private bool _uiShouldUpdate;
        
        //TODO should go to model
        [SerializeField] private float lerpSpeed;

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
            var eventArgs = new HudStatBarInitializedEventArgs
            {
                Key = gameObject.tag
            };
            OnStatInitialized(this, eventArgs);
        }
        
        
        private void Update()
        {
            HandleStatBar();
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