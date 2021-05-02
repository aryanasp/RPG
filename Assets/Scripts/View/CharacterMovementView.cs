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

    public class VelocityChangedInViewEventArgs : EventArgs
    {
        public Animator Animator { set; get; }
        public Vector2 Velocity { set; get; }
    }

    public interface ICharacterMovementView
    {
        event EventHandler<InitializedMovementEventArgs> OnMovementInitialize;
        event EventHandler<DestinationClickedEventArgs> OnDestinationClicked;
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

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class CharacterMovementView : MonoBehaviour, ICharacterMovementView, ICharacterMovementAnimation
    {
        private Rigidbody2D _rigidbody2D;

        // Implement ICharacterMovementView interface
        public event EventHandler<InitializedMovementEventArgs> OnMovementInitialize = (sender, e) => { };
        public event EventHandler<DestinationClickedEventArgs> OnDestinationClicked = (sender, e) => { };
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
                if (_rigidbody2D.velocity != value)
                {
                    var eventArgs = new VelocityChangedInViewEventArgs
                    {
                        Animator = GetComponent<Animator>(),
                        Velocity = value
                    };
                    _rigidbody2D.velocity = value;
                    OnVelocityChangedInView(this, eventArgs);
                }
            }
        }

        // Implement ICharacterMovementAnimation interface
        public event EventHandler<VelocityChangedInViewEventArgs> OnVelocityChangedInView = (sender, e) => { };


        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            var eventArgs = new InitializedMovementEventArgs
            {
            };
            OnMovementInitialize(this, eventArgs);
        }


        private void Update()
        {
            CheckIfReachedToDestination();
            HandleInput();
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

        private void HandleInput()
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (Camera.main)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    var mainCameraTransform = Camera.main.transform;
                    var alpha = mainCameraTransform.parent.localEulerAngles.z;
                    var theta = mainCameraTransform.localEulerAngles.x;
                    var alphaRad = alpha * Mathf.Deg2Rad;
                    var thetaRad = theta * Mathf.Deg2Rad;
                    var xx = worldPosition.x - (worldPosition.z * (Mathf.Sin(alphaRad) * Mathf.Sin(thetaRad)) /
                        Mathf.Cos(thetaRad));
                    var yy =  worldPosition.y + (worldPosition.z * (Mathf.Cos(alphaRad) * Mathf.Sin(thetaRad)) /
                                                 Mathf.Cos(thetaRad));
                    
                    var targetEventArgs = new DestinationClickedEventArgs
                    {
                        Destination = new Vector2(xx, yy)
                    };
                    OnDestinationClicked(this, targetEventArgs);
                }
            }
        }
    }
}