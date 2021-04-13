using System.Collections.Generic;
using Model;
using UnityEngine;
using View;

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
            public HudCharacterControllerFactory(IHudModel hudModel, IHudImageView hudImageView,
                List<IHudStatView> hudStatViews)
            {
                foreach (var hudStatView in hudStatViews)
                {
                    _controllers.Add(new HudController(hudModel, hudImageView, hudStatView));
                }
            }
        }
    }
}