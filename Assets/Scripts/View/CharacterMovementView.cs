using System;
using UnityEngine;

namespace View
{
    public class InitializedMovementEventArgs : EventArgs
    {
        
    }
    public class DestinationClickedEventArgs : EventArgs
    {
        public Vector2 Destination { set; get; }
    }

    public class DestinationReachedEventArgs : EventArgs
    {
        
    }

    public interface ICharacterMovementView
    {
        event EventHandler<InitializedMovementEventArgs> OnMovementInitialize;
        event EventHandler<DestinationClickedEventArgs> OnDestinationClicked;
        event EventHandler<DestinationReachedEventArgs> OnDestinationReached;
        Vector2 Position {set; get; }
        Vector3 Rotation { set; }
        Vector2 Destination { set; }
        Vector2 Velocity { set; }
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovementView : MonoBehaviour, ICharacterMovementView
    {
        public event EventHandler<InitializedMovementEventArgs> OnMovementInitialize = (sender, e) => { };
        public event EventHandler<DestinationClickedEventArgs> OnDestinationClicked = (sender, e) => { };
        public event EventHandler<DestinationReachedEventArgs> OnDestinationReached = (sender, e) => { };
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            var eventArgs = new InitializedMovementEventArgs
            {
                
            };
            OnMovementInitialize(this, eventArgs);
        }
        
        public Vector2 Position
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
            set => _rigidbody2D.velocity = value;
        }

        private void Update()
        {
            CheckIfReachedToDestination();
            HandleInput();
        }

        private void CheckIfReachedToDestination()
        {
            if (Mathf.Abs(Destination.x - transform.position.x) < 0.1f &&
                Mathf.Abs(Destination.y - transform.position.y) < 0.1f)
            {
                var eventArgs = new DestinationReachedEventArgs
                {
                    
                };
                OnDestinationReached(this, eventArgs);
            }
        }

        private void HandleInput()
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (Camera.main)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    var targetEventArgs = new DestinationClickedEventArgs
                    {
                        Destination = new Vector2(worldPosition.x, worldPosition.y),
                    };
                    OnDestinationClicked(this, targetEventArgs);
                }
            }
        }
    }
}