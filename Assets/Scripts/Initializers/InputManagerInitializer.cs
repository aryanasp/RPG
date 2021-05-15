using Configs;
using Controller;
using Model;
using View;

namespace Initializers
{
    public class InputManagerInitializer
    {
        public InputManagerInitializer(InputConfigs inputConfigs, IControllableCharacterData _characterData)
        {
            var movementInputView = UnityEngine.Object.FindObjectOfType<MovementInputView>();
            if (movementInputView == null)
            {
                movementInputView = UnityEngine.Object.Instantiate(inputConfigs.InputManagerPrefab).GetComponent<MovementInputView>();
            }
            var movementInputController = new MovementInputController(movementInputView, _characterData);
        }
    }
}