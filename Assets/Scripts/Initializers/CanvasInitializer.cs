using Configs;
using Controller;
using Model;
using UnityEngine;
using View;

namespace Initializers
{
    public interface ICanvasInitializer
    {
        
    }

    public class CanvasInitializer : ICanvasInitializer
    {
        public CanvasInitializer(CanvasConfig config)
        {
            // Initialize Hud GameObject
            GameObject canvasGameObject = UnityEngine.Object.Instantiate(config.CanvasPrefab);
            
            
            // Initialize models and views from factories
            var hudCharacterModel = new ModelFactory.HudCharacterModelFactory().Model;
            var hudCharacterView = new ViewFactory.HudCharacterViewFactory().View;
            var hudStatViews = new ViewFactory.HudStatViewFactory().Views;
            // Initialize controllers from factory
            var hudControllers =
                new ControllerFactory.HudCharacterControllerFactory(hudCharacterModel, hudCharacterView, hudStatViews)
                    .Controllers;
        }
    }
}