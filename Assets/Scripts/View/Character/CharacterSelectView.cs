using System;
using UnityEngine;
using View.Hud;

namespace View.Character
{
    public class CharacterSelectEventArgs
    {
        
    }
    
    public interface ICharacterSelectView
    {
        event EventHandler<CharacterSelectEventArgs> OnCharacterClicked;
    }
    public class CharacterSelectView : MonoBehaviour, ICharacterSelectView
    {
        private HudImageView _hudImageView;
        public event EventHandler<CharacterSelectEventArgs> OnCharacterClicked = (sender, e) => { };
        // Start is called before the first frame update
        void Start()
        {
            _hudImageView = FindObjectOfType<HudImageView>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    
        private void OnMouseDown()
        {
            if (Input.GetMouseButton(0))
            {
                var eventArgs = new CharacterSelectEventArgs
                {
                    
                };
                OnCharacterClicked(this, eventArgs);
            }
        }

        
    }
}
