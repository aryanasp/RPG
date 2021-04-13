using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class ViewFactory
    {
        
        // Interface for the view factory
        public interface IHudCharacterViewFactory
        {
            // Get the created view
            IHudImageView View { get; }
        }
        

        // Implementation of the HudCharacterViewFactory factory
        public class HudCharacterViewFactory : IHudCharacterViewFactory
        {
            public IHudImageView View { get; private set; }

            // Create the view
            public HudCharacterViewFactory()
            {
                var instance = UnityEngine.Object.FindObjectOfType<HudImageView>();
                View = instance.gameObject.GetComponent<IHudImageView>();
            }
            
        }
        
        public interface IHudStatViewFactory
        {   
            // Get the created views
            List<IHudStatView> Views { get; }
        }
        
        // Implementation of the HudStatViewFactory factory
        public class HudStatViewFactory : IHudStatViewFactory
        {
            private List<IHudStatView> _views = new List<IHudStatView>();

            public List<IHudStatView> Views
            {
                get => _views;
            }
            
            // Create the views
            public HudStatViewFactory()
            {
                var instances = UnityEngine.Object.FindObjectsOfType<HudStatView>();
                foreach (var hudStatView in instances)
                {
                    if (hudStatView.gameObject.layer == LayerMask.NameToLayer("UI"))
                    {
                        _views.Add(hudStatView.GetComponent<IHudStatView>());
                    }
                }
            }
        }
        
        public interface ICharacterStatViewFactory
        {
            List<ICharacterStatView> Views { get; }
        }
        
        public class CharacterStatViewFactory
        {
            private List<ICharacterStatView> _views = new List<ICharacterStatView>();

            public List<ICharacterStatView> Views
            {
                get => _views;
            }

            public CharacterStatViewFactory(GameObject characterGameObject)
            {
                var instances = characterGameObject.GetComponentsInChildren<ICharacterStatView>();
                foreach (var characterStatView in instances)
                {
                    _views.Add(characterStatView);
                }
            }
        }
    }
}