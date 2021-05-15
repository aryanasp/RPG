using System;
using UnityEngine;

namespace View
{
    public class DestinationClickedEventArgs : EventArgs
    {
        public Vector2 Destination { set; get; }
    }

    public interface IMovementInputView
    {
        event EventHandler<DestinationClickedEventArgs> OnDestinationClicked;
    }
    
    public class MovementInputView : MonoBehaviour, IMovementInputView
    {
        public event EventHandler<DestinationClickedEventArgs> OnDestinationClicked = (sender, e) => { };

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
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
        private void Update()
        {
            HandleInput();
        }
    }
}