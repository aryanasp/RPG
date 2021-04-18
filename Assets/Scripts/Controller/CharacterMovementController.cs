using Model;
using UnityEngine;
using View;

namespace Controller
{
    public interface ICharacterMovementController
    {
    }

    public class CharacterMovementController : ICharacterMovementController
    {
        private ICharacterMovementModel _characterMovementModel;
        private ICharacterMovementView _characterMovementView;

        public CharacterMovementController(ICharacterMovementModel characterMovementModel,
            ICharacterMovementView characterMovementView)
        {
            _characterMovementModel = characterMovementModel;
            _characterMovementView = characterMovementView;

            characterMovementView.OnMovementInitialize += HandleInitializeParams;
            characterMovementView.OnDestinationReached += HandleDestinationReached;
            characterMovementView.OnDestinationClicked += HandleDestinationClicked;
            characterMovementModel.OnVelocityChanged += HandleVelocityChanged;
            SyncDestinations();
        }

        private void HandleInitializeParams(object sender, InitializedMovementEventArgs e)
        {
            _characterMovementView.Position = _characterMovementModel.Destination;
            _characterMovementView.Rotation = _characterMovementModel.Rotation;
            _characterMovementView.Destination = _characterMovementModel.Destination;
        }

        private void SyncDestinations()
        {
            _characterMovementView.Destination = _characterMovementModel.Destination;
        }

        private void HandleDestinationClicked(object sender, DestinationClickedEventArgs e)
        {
            _characterMovementModel.Destination = e.Destination;
            SyncDestinations();
            _characterMovementModel.IsInDestination = false;
            FindRoadToDestination();
        }

        private void FindRoadToDestination()
        {
            var currentPos = _characterMovementView.Position;
            var destination = _characterMovementModel.Destination;
            if (!_characterMovementModel.IsInDestination)
            {
                _characterMovementModel.MoveDirection = (destination - currentPos).normalized;
            }
        }

        private void HandleVelocityChanged(object sender, OnVelocityChangedEventArgs e)
        {
            _characterMovementView.Velocity = e.Velocity;
            _characterMovementView.Destination = e.Destination;
        }

        private void HandleDestinationReached(object sender, DestinationReachedEventArgs e)
        {
            _characterMovementModel.IsInDestination = true;
        }
    }
}