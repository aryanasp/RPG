using System.Collections.Generic;
using Configs;
using Controller;
using Model;
using UnityEngine;
using View;
using View.Character;

namespace Initializers
{
    public interface ICharacterInitializer
    {
    }

    public class CharacterInitializer : ICharacterInitializer
    {
        // Initialize Character Stat
        // Initialize model and view for a character stats
        public CharacterInitializer(ISelectedCharacterData selectedCharacterData,
            IControllableCharacterData controllableCharacterData, CharacterConfig config, Vector3 position,
            Vector3 rotation)
        {
            // Instantiate character game object
            GameObject characterGameObject =
                UnityEngine.Object.FindObjectOfType<CharacterSelectView>()?.gameObject;

            if (characterGameObject == null)
            {
                characterGameObject =
                    UnityEngine.Object.Instantiate(config.CharacterPrefab, position, Quaternion.identity);
            }

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

            // Initialize character movement
            // Get movement initial values from config object
            var characterMovementModel =
                new ModelFactory.CharacterMovementModelFactory(position, rotation, config.MovementSpeed)
                    .Model; // get base move speed from config
            var characterMovementView = new ViewFactory.CharacterMovementViewFactory(characterGameObject).View;
            var characterMovementController =
                new ControllerFactory.CharacterMovementControllerFactory(characterMovementModel, characterMovementView)
                    .Controller;

            // TODO add new mechanics here


            // Initialize new character with above mechanics
            var characterModel = new CharacterModel(characterGameObject, config.HudImage, characterStatModel,
                characterMovementModel);

            var characterSelectView = characterGameObject.GetComponent<CharacterSelectView>();
            var characterSelectController = new CharacterSelectController(characterSelectView, characterModel, selectedCharacterData, controllableCharacterData);
        }
    }
}