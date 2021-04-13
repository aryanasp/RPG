using System.Collections.Generic;
using Model;
using UnityEngine;
using View;

namespace Controller
{
    
    public interface ICharacterStatController
    {
            
    }
    
    public class CharacterStatController : ICharacterStatController
    {
        // Keep references to the model and view
        private readonly ICharacterStatModel _characterStatModel;
        private readonly ICharacterStatView _characterStatView;

        // Controller depends on interfaces for the model and view
        public CharacterStatController(ICharacterStatModel characterStatModel, ICharacterStatView characterStatView)
        {
            this._characterStatModel = characterStatModel;
            this._characterStatView = characterStatView;
            
            // Listen to input from the view
            characterStatView.OnStatInitialized += HandleStatInitializedFromViewInput;

            characterStatView.OnStatChanged += HandleStatChanged;
            // Listen to changes in the model
            characterStatModel.OnStatChanged += HandleCharacterStatChanged;

            // Set the view's initial state by syncing with the model
            SyncStats();
        }

        private void HandleStatChanged(object sender, CharacterStatChangedEventArgs e)
        {
            _characterStatModel.ChangeStatValue(e.StatKey, e.ChangeAmount, e.IsIncreasingly, e.IsPercentile, e.IsFromMaxValue);
        }

        private void HandleStatInitializedFromViewInput(object sender, CharacterStatInitializedEventArgs e)
        {
            _characterStatModel.Initialize(e.Key, e.InitialValue, e.InitialMaxValue);
        }

        
        private void HandleCharacterStatChanged(object sender, CharacterStatEventArgs e)
        {
            
            if (_characterStatView.StatKey == e.Stat.Key)
            {
                var tempStatValues = new Dictionary<string, float>() { {"current", e.StatCurrentValue}, {"max", e.StatMaxValue}};
                _characterStatView.StatConfigs = tempStatValues;
            }
            SyncStats();
        }
        
        private void SyncStats()
        {
            if (_characterStatView.StatKey == _characterStatModel.Stat.Key)
            {
                var tempStatValues = new Dictionary<string, float>() { {"current", _characterStatModel.Stat.StatCurrentValue}, {"max", _characterStatModel.Stat.StatMaxValue}};
                _characterStatView.StatConfigs = tempStatValues;
            }
        }
        
    }
}