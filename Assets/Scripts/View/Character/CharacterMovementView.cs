using System;
using UnityEngine;

namespace View
{
    public class InitializedMovementEventArgs : EventArgs
    {
    }

    
    public class DestinationReachedEventArgs : EventArgs
    {
    }

    public class VelocityChangedInViewEventArgs : EventArgs
    {
        public Animator Animator { set; get; }
        public Vector2 Velocity { set; get; }
    }

    public interface ICharacterMovementView
    {
        event EventHandler<InitializedMovementEventArgs> OnMovementInitialize;
        event EventHandler<DestinationReachedEventArgs> OnDestinationReached;
        Vector3 Position { set; get; }
        Vector3 Rotation { set; }
        Vector2 Destination { set; }
        Vector2 Velocity { set; }
    }

    public interface ICharacterMovementAnimation
    {
        event EventHandler<VelocityChangedInViewEventArgs> OnVelocityChangedInView;
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class CharacterMovementView : MonoBehaviour, ICharacterMovementView, ICharacterMovementAnimation
    {
        private Rigidbody _rigidbody;

        // Implement ICharacterMovementView interface
        public event EventHandler<InitializedMovementEventArgs> OnMovementInitialize = (sender, e) => { };
        
        public event EventHandler<DestinationReachedEventArgs> OnDestinationReached = (sender, e) => { };

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Vector3 Rotation
        {
            set => transform.Rotate(value.x, value.y, value.z);
        }

        public Vector2 Destination { private get; set; }

        public Vector2 Velocity
        {
            set
            {
                if (_rigidbody.velocity != (Vector3) value)
                {
                    var eventArgs = new VelocityChangedInViewEventArgs
                    {
                        Animator = GetComponent<Animator>(),
                        Velocity = value
                    };
                    _rigidbody.velocity = value;
                    OnVelocityChangedInView(this, eventArgs);
                }
            }
        }

        // Implement ICharacterMovementAnimation interface
        public event EventHandler<VelocityChangedInViewEventArgs> OnVelocityChangedInView = (sender, e) => { };


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            var eventArgs = new InitializedMovementEventArgs
            {
            };
            OnMovementInitialize(this, eventArgs);
        }


        private void Update()
        {
            CheckIfReachedToDestination();
        }

        private void CheckIfReachedToDestination()
        {   
            //TODO tolerance differs with scaler which is in model
            if (Mathf.Abs(Destination.x - transform.position.x) < 4f &&
                Mathf.Abs(Destination.y - transform.position.y) < 4f)
            {
                var eventArgs = new DestinationReachedEventArgs
                {
                };
                OnDestinationReached(this, eventArgs);
            }
        }

        
        
    }
}