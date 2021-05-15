using System;
using System.Collections.Generic;

namespace Model
{
    
    public class ConfirmIdEventArgs : EventArgs
    {
            
    }
    
    public interface IControllableCharacterData
    {
        event EventHandler<ConfirmIdEventArgs> OnConfirmId;
        int Id { set; get; }
        List<int> ControllableCharacterIds { set; get; }
        ICharacterMovementModel CharacterMovementModel { set; get;}
    }
    public class ControllableCharacterData : IControllableCharacterData
    {
        private int _id;

        public event EventHandler<ConfirmIdEventArgs> OnConfirmId = (sender, e) => { };

        public int Id
        {
            get => _id;
            set
            {
                if(ControllableCharacterIds.Contains(value))
                {
                    _id = value;
                    var eventArgs = new ConfirmIdEventArgs
                    {

                    };
                    OnConfirmId(this, eventArgs);
                }
            }
        }
        public List<int> ControllableCharacterIds { get; set; }
        public ICharacterMovementModel CharacterMovementModel { get; set; }

        public ControllableCharacterData()
        {
            ControllableCharacterIds = new List<int>();
            if (CharactersData.CharacterModels.Count > 0)
            {
                var characterModel = CharactersData.CharacterModels[0];
                Id = characterModel.Id;
                ControllableCharacterIds.Add(Id);
                CharacterMovementModel = characterModel.CharacterMovementModel;
            }
            else
            {
                _id = 0;
                ControllableCharacterIds.Add(Id);
                CharacterMovementModel = null;
            }
        }
    }
}