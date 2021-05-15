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
        public CanvasInitializer(CanvasConfig config, ISelectedCharacterData selectedCharacterData)
        {
            // Initialize Hud GameObject
            GameObject canvasGameObject = UnityEngine.Object.FindObjectOfType<CanvasView>()?.gameObject;

            if (canvasGameObject == null)
            {
                canvasGameObject = UnityEngine.Object.Instantiate(config.CanvasPrefab);
            }
            
            // Initialize models and views from factories
            var hudCharacterView = new ViewFactory.HudCharacterViewFactory().View;
            var hudStatViews = new ViewFactory.HudStatViewFactory().Views;
            // Initialize controllers from factory
            var hudControllers =
                new ControllerFactory.HudCharacterControllerFactory(selectedCharacterData, hudCharacterView, hudStatViews)
                    .Controllers;
        }
    }
}