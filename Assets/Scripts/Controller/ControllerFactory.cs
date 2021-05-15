using System.Collections.Generic;
using Model;
using UnityEngine;
using View;
using View.Hud;

namespace Controller
{
    public class ControllerFactory
    {
        // Interface of the view factory
        public interface IHudCharacterControllerFactory
        {
            // Get the created controller
            List<IHudController> Controllers { get; }
        }

        // Implementation of the controller factory
        public class HudCharacterControllerFactory : IHudCharacterControllerFactory
        {
            private List<IHudController> _controllers = new List<IHudController>();

            public List<IHudController> Controllers
            {
                get => _controllers;
            }

            // Create just the controller
            public HudCharacterControllerFactory(ISelectedCharacterData selectedCharacterData, IHudImageView hudImageView,
                List<IHudStatView> hudStatViews)
            {
                foreach (var hudStatView in hudStatViews)
                {
                    _controllers.Add(new HudController(selectedCharacterData, hudImageView, hudStatView));
                }
            }
        }

        public interface ICharacterStatControllerFactory
        {
            List<ICharacterStatController> Controllers { get; }
        }

        public class CharacterStatControllerFactory : ICharacterStatControllerFactory
        {
            private List<ICharacterStatController> _controllers = new List<ICharacterStatController>();

            public List<ICharacterStatController> Controllers
            {
                get => _controllers;
            }

            public CharacterStatControllerFactory(ICharacterStatModel characterStatModel,
                List<ICharacterStatView> characterStatViews)
            {
                foreach (var characterStatView in characterStatViews)
                {
                    _controllers.Add(new CharacterStatController(characterStatModel, characterStatView));
                }
            }
        }

        public interface ICharacterMovementControllerFactory
        {
            ICharacterMovementController Controller { get; }
        }

        public class CharacterMovementControllerFactory : ICharacterMovementControllerFactory
        {
            public ICharacterMovementController Controller { get; private set; }
            public CharacterMovementControllerFactory(ICharacterMovementModel characterMovementModel,
                ICharacterMovementView characterMovementView)
            {
                Controller = new CharacterMovementController(characterMovementModel, characterMovementView);
            }
        }
    }
}