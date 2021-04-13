using UnityEngine;

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
    }
}
