using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    // Dispatched when the character is clicked
    public class CharacterClickedEventArgs : EventArgs
    {
        public GameObject Character;
    }

    // Interface for the hud character view
    public interface IHudImageView
    {
        // Dispatched when character clicked
        event EventHandler<CharacterClickedEventArgs> OnClicked;
        
        // Set the hud character
        GameObject Character { set; }
        Sprite HudCharacterImage { set; }
    }
    
    // Implementation of the hud character view
    public class HudImageView : MonoBehaviour, IHudImageView
    {

        [SerializeField]
        private Image hudCharacterImage;

        public Sprite HudCharacterImage
        {
            set
            {
                // Set the image sprite
                hudCharacterImage.sprite = value;
            }
        }
        
        private GameObject _character;
        
        // Dispatched when Character clicked
        public event EventHandler<CharacterClickedEventArgs> OnClicked = (sender, e) => { };
        
        //TODO Set the hud's character
        public GameObject Character 
        {
            set
            {
                _character = value;
                var eventArgs = new CharacterClickedEventArgs {Character = _character};
                OnClicked(this, eventArgs);
            }
        }
    }
}