using UnityEngine;

namespace View
{
    public class CharacterView : MonoBehaviour
    {
        private HudImageView _hudImageView;
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
                _hudImageView.Character = gameObject;
            }
        }
    }
}
