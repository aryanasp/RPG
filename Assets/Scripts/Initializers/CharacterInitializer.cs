using System.Collections.Generic;
using Configs;
using Controller;
using Model;
using UnityEngine;
using View;

namespace Initializers
{
    public interface ICharacterInitializer
    {
    }

    public class CharacterInitializer : ICharacterInitializer
    {
        // Initialize Character Stat
        // Initialize model and view for a character stats
        public CharacterInitializer(CharacterConfig config, Vector3 position)
        {
            // Instantiate character game object
            GameObject characterGameObject =
                UnityEngine.Object.Instantiate(config.CharacterPrefab, position, Quaternion.identity);


            // Initialize character stat
            // Get stat initial values from config object
            var characterStatModel = new ModelFactory.CharacterStatModelFactory(
                    // Get health initial values from config object
                    new[]
                    {
                        config.HealthInitialValue, config.MaxHealthInitialValue
                    },
                    // Get mana initial values from config object
                    new[]
                    {
                        config.ManaInitialValue, config.MaxManaInitialValue
                    })
                .Model;
            var characterStatViews = new ViewFactory.CharacterStatViewFactory(characterGameObject).Views;
            var characterStatControllers =
                new ControllerFactory.CharacterStatControllerFactory(characterStatModel, characterStatViews)
                    .Controllers;

            // TODO add new mechanics here


            // Initialize new character with above mechanics
            var character = new CharacterModel(characterGameObject, characterStatModel);
        }
    }
}