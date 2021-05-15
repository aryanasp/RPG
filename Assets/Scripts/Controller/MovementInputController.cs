using Model;
using View;

namespace Controller
{
    public class MovementInputController
    {
        private IMovementInputView _movementInputView;
        private IControllableCharacterData _controllableCharacterData;

        public MovementInputController(IMovementInputView movementInputView, IControllableCharacterData controllableCharacterData)
        {
            _movementInputView = movementInputView;
            _controllableCharacterData = controllableCharacterData;
            _movementInputView.OnDestinationClicked += HandleDestinationClicked;
        }
        
        private void HandleDestinationClicked(object sender, DestinationClickedEventArgs e)
        {
            if (_controllableCharacterData.CharacterMovementModel == null) return;
            _controllableCharacterData.CharacterMovementModel.Destination = e.Destination;
            _controllableCharacterData.CharacterMovementModel.IsInDestination = false;
        }

    }
}