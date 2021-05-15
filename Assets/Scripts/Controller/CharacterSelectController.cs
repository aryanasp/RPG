using Model;
using UnityEngine;
using View.Character;

namespace Controller
{
    public class CharacterSelectController
    {
        private ICharacterSelectView _characterSelectView;
        private ICharacterModel _characterModel;
        private ISelectedCharacterData _selectedCharacterData;
        private IControllableCharacterData _controllableCharacterData;
         
        public CharacterSelectController(ICharacterSelectView characterSelectView, ICharacterModel characterModel, ISelectedCharacterData selectedCharacterData, IControllableCharacterData controllableCharacterData)
        {
            _characterSelectView = characterSelectView;
            _characterModel = characterModel;
            _selectedCharacterData = selectedCharacterData;
            _controllableCharacterData = controllableCharacterData;
            _characterSelectView.OnCharacterClicked += HandleCharacterClicked;
            _controllableCharacterData.OnConfirmId += HandleIdConfirmed;
        }

        private void HandleIdConfirmed(object sender, ConfirmIdEventArgs e)
        {
            _controllableCharacterData.CharacterMovementModel = _characterModel.CharacterMovementModel;
        }

        private void HandleCharacterClicked(object sender, CharacterSelectEventArgs e)
        {
            SyncSelectedCharacter();
            _controllableCharacterData.Id = _characterModel.Id;
        }

        private void SyncSelectedCharacter()
        {
            _selectedCharacterData.Id = _characterModel.Id;
            _selectedCharacterData.HudImageSprite = _characterModel.HudImageSprite;
            _selectedCharacterData.CharacterStatModel = _characterModel.CharacterStatModel;
            _selectedCharacterData.CharacterSwitchingFinished();
        }
    }
}