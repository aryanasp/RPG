using System;
using UnityEngine;

namespace View
{
    public class MouseOverBoundaryEventArgs
    {
        public string DirectionName { get; set; }
        public Vector3 DirectionVector { get; set; }
    }
    
    public interface IBoundariesView
    {
        event EventHandler<MouseOverBoundaryEventArgs> OnMouseOverBoundary;
    }
    public class BoundariesView : MonoBehaviour, IBoundariesView
    {
        public event EventHandler<MouseOverBoundaryEventArgs> OnMouseOverBoundary = (sender, e) => {};
        private IBoundarySideView _boundarySideView;

        private void Start()
        {
            _boundarySideView = GetComponent<IBoundarySideView>();
        }

        private void OnMouseEnter()
        {
            var eventArgs = new MouseOverBoundaryEventArgs
            {
                DirectionName = _boundarySideView.BoundarySide,
                DirectionVector = _boundarySideView.BoundarySideVector
            };
            OnMouseOverBoundary(this, eventArgs);
        }

        private void OnMouseExit()
        {
            var eventArgs = new MouseOverBoundaryEventArgs
            {
                DirectionName = "Not " + _boundarySideView.BoundarySide,
                DirectionVector = -_boundarySideView.BoundarySideVector
            };
            OnMouseOverBoundary(this, eventArgs);
        }
    }
}