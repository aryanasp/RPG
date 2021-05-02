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
        Vector3 Destination { set; get; }
        Vector3 Rotation { set; get; }
        bool IsInDestination { set; get; }
        Vector2 MoveDirection { set; get; }
    }

    public class CharacterMovementModel : ICharacterMovementModel
    {
        private readonly float _moveSpeed;
        //TODO hard coded should change with animations pixel/unit and should get from scriptable objects
        private float _scaler;
        private Vector2 _moveDirection;
        private bool _isInDestination;
        
        // Implement interface
        public event EventHandler<OnVelocityChangedEventArgs> OnVelocityChanged = (sender, e) => { };
        public Vector3 Destination { get; set; }

        public Vector3 Rotation { get; set; }

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
            get => _moveDirection;
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
            _scaler = 4;
            Destination = position;
            Rotation = rotation;
            _isInDestination = true;
            _moveSpeed = moveSpeed / _scaler;
        }
    }
}