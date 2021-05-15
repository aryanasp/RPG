using System;
using UnityEngine;

namespace Model
{
    public class SwitchCharacterFinishEventArgs : EventArgs
    {
        
    }
    public interface ISelectedCharacterData
    {
        event EventHandler<SwitchCharacterFinishEventArgs> OnSwitchingCharacterFinished; 
        int Id { set; get; }
        Sprite HudImageSprite { set; get; }
        ICharacterStatModel CharacterStatModel { set; get; }
        void CharacterSwitchingFinished();
    }

    public class SelectedCharacterData : ISelectedCharacterData
    {
        public event EventHandler<SwitchCharacterFinishEventArgs> OnSwitchingCharacterFinished = (sender, e) => { };
        public int Id { get; set; }
        public Sprite HudImageSprite { get; set; }
        public ICharacterStatModel CharacterStatModel { get; set; }
        

        public void CharacterSwitchingFinished()
        {
            var eventArgs = new SwitchCharacterFinishEventArgs
            {

            };
            OnSwitchingCharacterFinished(this, eventArgs);
        }

        public SelectedCharacterData()
        {
            if (CharactersData.CharacterModels.Count > 0)
            {
                var characterModel = CharactersData.CharacterModels[0];
                Id = characterModel.Id;
                HudImageSprite = characterModel.HudImageSprite;
                CharacterStatModel = characterModel.CharacterStatModel;
            }
            else
            {
                Id = 0;
                HudImageSprite = Resources.Load<Sprite>("Sprites/Hud/Wizard.png");
                CharacterStatModel = null;
            }
        }
        
    }
}