using System;
using System.Collections.Generic;
using Model;
using UnityEngine;
using View;

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
        private readonly IHudModel _hudModel;
        private ICharacterStatModel _characterStatModel;
        private readonly IHudImageView _hudImageView;
        private readonly IHudStatView _hudStatView;

        // Controller depends on interfaces for the model and view
        public HudController(IHudModel hudModel, IHudImageView hudImageView, IHudStatView hudStatView)
        {
            this._hudModel = hudModel;
            this._hudImageView = hudImageView;
            this._hudStatView = hudStatView;
            this._characterStatModel = CharactersInfoModel.CharacterModels[0].CharacterStatModel;

            hudModel.Character = CharactersInfoModel.CharacterModels[0].Character;

            // Listen to input from the view
            hudImageView.OnClicked += HandleClicked;

            // Listen to changes in the model
            hudModel.OnHudCharacterChanged += HandleHudCharacterChanged;

            hudStatView.OnStatInitialized += HandleHudStatInitialized;

            _characterStatModel.OnStatChanged += HandleStatChanged;

            // Set the view's initial state by syncing with the model
            SyncCharacters();
        }

        private void HandleStatChanged(object sender, CharacterStatEventArgs e)
        {
            if (_hudModel.Character == e.Character)
            {
                if (_hudStatView.StatKey == e.Stat.Key)
                {
                    var tempStatValues = new Dictionary<string, float>()
                        {{"current", e.StatCurrentValue}, {"max", e.StatMaxValue}};
                    _hudStatView.StatConfigs = tempStatValues;
                }  
            }
        }

        // Called when the view is clicked
        private void HandleClicked(object sender, CharacterClickedEventArgs e)
        {
            //TODO
            // Do something to the model
            _hudModel.Character = e.Character;
        }

        private void HandleHudCharacterChanged(object sender, HudCharacterChangedEventArgs e)
        {
            SyncCharacters();
            _hudImageView.HudCharacterImage = e.HudImage;
            foreach (var characterModel in CharactersInfoModel.CharacterModels)
            {
                if (characterModel.Character == _hudModel.Character)
                {
                    _characterStatModel.OnStatChanged -= HandleStatChanged;
                    _characterStatModel = characterModel.CharacterStatModel;
                    _characterStatModel.OnStatChanged += HandleStatChanged;
                }
            }
        }

        private void SyncCharacters()
        {
            _hudImageView.Character = _hudModel.Character;
            _hudStatView.Character = _hudModel.Character;
            foreach (var characterModel in CharactersInfoModel.CharacterModels)
            {
                if (characterModel.Character == _hudModel.Character)
                {
                    this._characterStatModel = characterModel.CharacterStatModel;
                }
            }

            foreach (var characterModel in CharactersInfoModel.CharacterModels)
            {
                if (characterModel.Character == _hudModel.Character)
                {
                    // Introduce variable for character stat model
                    var characterStatModel = characterModel.CharacterStatModel;
                    characterModel.CharacterStatModel.SwitchStat(_hudStatView.StatKey);
                    var temp = new Dictionary<string, float>()
                    {
                        {"current", characterStatModel.Stat.StatCurrentValue},
                        {"max", characterStatModel.Stat.StatMaxValue}
                    };
                    _hudStatView.StatConfigs = temp;
                }
            }
        }

        private void HandleHudStatInitialized(object sender, HudStatBarInitializedEventArgs e)
        {
            foreach (var characterModel in CharactersInfoModel.CharacterModels)
            {
                if (characterModel.Character == _hudModel.Character)
                {
                    // Introduce variable for character stat model
                    var characterStatModel = characterModel.CharacterStatModel;
                    characterModel.CharacterStatModel.SwitchStat(e.Key);
                    var temp = new Dictionary<string, float>()
                    {
                        {"current", characterStatModel.Stat.StatCurrentValue},
                        {"max", characterStatModel.Stat.StatMaxValue}
                    };
                    _hudStatView.StatConfigs = temp;
                }
            }
        }
    }
}