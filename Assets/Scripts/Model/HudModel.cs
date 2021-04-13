using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    // Dispatched when the hud character changes
    public class HudCharacterChangedEventArgs : EventArgs
    {
        public Sprite HudImage { set; get; }
    }

    // Interface for the model
    public interface IHudModel
    {
        // Dispatched when the character changes
        event EventHandler<HudCharacterChangedEventArgs> OnHudCharacterChanged;
        
        // Hud character
        GameObject Character { get; set; }
    }
    
    // Implementation of the character model interface
    public class HudModel : IHudModel
    {
        private Dictionary<string, Sprite> _hudCharacterImageDictionary;
        
        public HudModel()
        {
            
            var images = Resources.LoadAll<Sprite>("Sprites/Hud/");
            _hudCharacterImageDictionary = new Dictionary<string, Sprite>();
            foreach (var hudCharacterImage in images)
            {
                _hudCharacterImageDictionary[hudCharacterImage.name] = hudCharacterImage;
            }
            
        }
        
        public event EventHandler<HudCharacterChangedEventArgs> OnHudCharacterChanged = (sender, e) => {};
        
        // Backing field for the hud character
        private GameObject _character;

        public GameObject Character
        {
            get => _character;
            set
            {
                // Only if character changed
                if (_character == value) return;
                // Set new character
                _character = value;
                    
                // Dispatch the 'hud character' event
                var eventArgs = new HudCharacterChangedEventArgs
                {
                    HudImage = _hudCharacterImageDictionary[_character.tag]
                };
                OnHudCharacterChanged(this, eventArgs);
            }
        }
    }
}