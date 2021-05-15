using UnityEngine;
using UnityEngine.UI;

namespace View.Hud
{
    // Interface for the hud character view
    public interface IHudImageView
    {
        Sprite HudCharacterImage { set; }
    }
    
    // Implementation of the hud character view
    public class HudImageView : MonoBehaviour, IHudImageView
    {
        [SerializeField]
        private Image hudCharacterImage;

        public Sprite HudCharacterImage
        {
            set =>
                // Set the image sprite
                hudCharacterImage.sprite = value;
        }
    }
}