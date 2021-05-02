using System;
using UnityEngine;

namespace Model
{
    public class CharacterAttackEventArgs
    {
        
    }
    public interface ICharacterAttackModel
    {
        event EventHandler<CharacterAttackEventArgs> OnCharacterAttacked; 
        bool Attack { set; get; }
    }
    
    public class CharacterAttackModel : ICharacterAttackModel
    {
        private bool _attack;
        public event EventHandler<CharacterAttackEventArgs> OnCharacterAttacked = (sender, e) => {};

        public bool Attack
        {
            get
            {
                return _attack;
            }
            set
            {
                _attack = value;
                var eventArgs = new CharacterAttackEventArgs
                {

                };
                OnCharacterAttacked(this, eventArgs);
            }
        }
    }
}