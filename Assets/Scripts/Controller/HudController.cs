using System;
using System.Collections.Generic;
using Model;
using UnityEngine;
using View;
using View.Hud;

namespace Controller
{
    // Interface for the hud character controller
    public interface IHudController
    {
        
    }

    // Implementation of the hud character controller
    public class HudController : IHudController
    {
        // Keep references to the model and view
        private ISelectedCharacterData _selectedCharacterData;
        private ICharacterStatModel _characterStatModel;
        private readonly IHudImageView _hudImageView;
        private readonly IHudStatView _hudStatView;

        // Controller depends on interfaces for the model and view
        public HudController(ISelectedCharacterData selectedCharacterData, IHudImageView hudImageView,
            IHudStatView hudStatView)
        {
            _selectedCharacterData = selectedCharacterData;
            _hudImageView = hudImageView;
            _hudStatView = hudStatView;
            _selectedCharacterData.OnSwitchingCharacterFinished += HandleSwitchingCharacter;
            if (_selectedCharacterData.CharacterStatModel == null) return;
            _characterStatModel = selectedCharacterData.CharacterStatModel;
            _characterStatModel.OnStatChanged += HandleStatChanged;
            _characterStatModel.InitializeStats();
        }

        private void HandleSwitchingCharacter(object sender, SwitchCharacterFinishEventArgs e)
        {
            if (_selectedCharacterData.CharacterStatModel == null) return;
            if (_characterStatModel != null)
            {
                _characterStatModel.OnStatChanged -= HandleStatChanged;
            }
            _characterStatModel = _selectedCharacterData.CharacterStatModel;
            _hudImageView.HudCharacterImage = _selectedCharacterData.HudImageSprite;
            _characterStatModel.OnStatChanged += HandleStatChanged;
            _characterStatModel.InitializeStats();
        }
        
        private void HandleStatChanged(object sender, CharacterStatEventArgs e)
        {
            if (_hudStatView.StatName == e.Stat.Name)
            {
                var tempStatValues = new Dictionary<string, float>()
                    {{"current", e.Stat.StatCurrentValue}, {"max", e.Stat.StatMaxValue}};
                _hudStatView.StatConfigs = tempStatValues;
            }
        }
    }
}