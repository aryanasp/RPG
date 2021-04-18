﻿using UnityEngine;

namespace Model
{
    public class ModelFactory
    {
        // Interface for the model factory
        public interface IHudCharacterModelFactory
        {
            // Get the created model
            IHudModel Model { get; }
        }

        // Implementation of the model factory
        public class HudCharacterModelFactory : IHudCharacterModelFactory
        {
            public IHudModel Model { get; private set; }

            // Create the model
            public HudCharacterModelFactory()
            {
                Model = new HudModel();
            }
        }
        
        public interface ICharacterStatModelFactory
        {
            // Get the created model
            ICharacterStatModel Model { get; }
        }

        public class CharacterStatModelFactory : ICharacterStatModelFactory
        {
            public ICharacterStatModel Model { get; private set; }

            // Create the model
            public CharacterStatModelFactory(float[] healthSetting, float[] manaSetting)
            {
                Model = new CharacterStatModel(healthSetting, manaSetting);
            }
        }
        
        public interface ICharacterMovementModelFactory
        {
            // Get the created Model
            ICharacterMovementModel Model { get; }
        }
        
        public class CharacterMovementModelFactory : ICharacterMovementModelFactory
        {
            public ICharacterMovementModel Model { get; private set; }
            
            // Create the model
            public CharacterMovementModelFactory(Vector3 position, Vector3 rotation, float moveSpeed)
            {
                Model = new CharacterMovementModel(position, rotation, moveSpeed);
            }
        }
    }
}
