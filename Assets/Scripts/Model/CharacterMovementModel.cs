using System;
using UnityEngine;

namespace Model
{
    public class OnVelocityChangedEventArgs : EventArgs
    {
        public Vector2 Velocity { get; set; }
        public Vector2 Destination { set; get; }
    }

    public interface ICharacterMovementModel
    {
        event EventHandler<OnVelocityChangedEventArgs> OnVelocityChanged;
        Vector2 Destination { set; get; }
        bool IsInDestination { set; get; }
        Vector2 MoveDirection { set; }
    }

    public class CharacterMovementModel : ICharacterMovementModel
    {
        private readonly float _moveSpeed;
        private Vector2 _moveDirection;
        private bool _isInDestination;
        
        // Implement interface
        public event EventHandler<OnVelocityChangedEventArgs> OnVelocityChanged = (sender, e) => { };
        public Vector2 Destination { get; set; }

        public bool IsInDestination
        {
            get => _isInDestination;
            set
            {
                if (_isInDestination != value)
                {
                    _isInDestination = value;
                    var eventArgs = new OnVelocityChangedEventArgs
                    {
                        Velocity =  _moveSpeed * _moveDirection * (_isInDestination? 0 : 1),
                        Destination = Destination
                    };
                    OnVelocityChanged(this, eventArgs);
                }
            }
        }
        public Vector2 MoveDirection
        {
            private get => _moveDirection;
            set
            {
                if (_moveDirection != (Vector2)value)
                {
                    _moveDirection = value.normalized;
                    var eventArgs = new OnVelocityChangedEventArgs
                    {
                        Velocity = _moveSpeed * _moveDirection,
                        Destination = Destination
                    };
                    OnVelocityChanged(this, eventArgs);
                }
            }
        }
        
        public CharacterMovementModel(Vector3 position, Vector3 rotation, float moveSpeed)
        {
            Destination = position;
            _isInDestination = true;
            _moveSpeed = moveSpeed;
        }
    }
}