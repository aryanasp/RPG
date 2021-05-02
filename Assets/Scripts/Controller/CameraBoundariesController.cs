using System;
using UnityEngine;
using View;

namespace Controller
{
    public interface ICameraBoundariesController
    {
    }

    public class CameraBoundariesController : ICameraBoundariesController
    {
        private ICameraView _cameraView;
        private IBoundariesView _boundariesView;

        public CameraBoundariesController(ICameraView cameraView, IBoundariesView boundariesView)
        {
            _cameraView = cameraView;
            _boundariesView = boundariesView;
            boundariesView.OnMouseOverBoundary += HandleMouseOverBoundary;
        }

        private void HandleMouseOverBoundary(object sender, MouseOverBoundaryEventArgs e)
        {
            var temp = new Vector3((float) Math.Sign(_cameraView.MoveVector.x),
                (float) Math.Sign(_cameraView.MoveVector.y),
                (float) Math.Sign(_cameraView.MoveVector.z));
            temp += e.DirectionVector;
            _cameraView.MoveVector = temp.normalized;
        }
    }
}