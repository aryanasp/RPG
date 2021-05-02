using System;
using UnityEngine;

namespace View
{
    public class MouseReachedToCameraLimitEventArgs
    {
    }

    public class CameraCreateEventArgs
    {
        
    }


    public interface ICameraView
    {
        event EventHandler<CameraCreateEventArgs> OnCameraCreate;
        Vector3 MoveVector { set; get; }

        void InitializeCamera(Transform mapPlane, float alpha, float theta, float cameraMovementSpeed,
            float cameraTurnRate);
    }

    public class CameraView : MonoBehaviour, ICameraView
    {
        private Vector3 _moveVector;
        private bool _cameraShouldMove;

        public event EventHandler<CameraCreateEventArgs> OnCameraCreate = (sender, e) => {};

        public Vector3 MoveVector
        {
            get { return _moveVector; }
            set
            {
                _moveVector = value;

                if (value != Vector3.zero)
                {
                    _cameraShouldMove = true;
                }
                else
                {
                    _cameraShouldMove = false;
                }
            }
        }

        private Transform _mapPlane;
        private float _cameraMovementSpeed;
        private float _cameraTurnRate;
        private Camera _camera;
        private Vector3 _mousePosition;

        // Angle between camera and outer central pivot around Z axis
        private float _alpha;

        // Camera rotation Angle around X axis
        private float _theta;
        
        private void Start()
        {
            _camera = GetComponent<Camera>();
            // Camera create message to camera data to send initialize data dont move it to awake
            var eventArgs = new CameraCreateEventArgs
            {

            };
            OnCameraCreate(this, eventArgs);
            _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        // Get angle with degree scale and convert it to radian scale
        public void InitializeCamera(Transform mapPlane, float alpha, float theta, float cameraMovementSpeed,
            float cameraTurnRate)
        {
            _mapPlane = mapPlane;
            _cameraTurnRate = cameraTurnRate;
            transform.parent.localEulerAngles = new Vector3(0, 0, alpha);
            _alpha = alpha * Mathf.Deg2Rad;
            transform.localEulerAngles = new Vector3(theta, 0, 0);
            _theta = theta * Mathf.Deg2Rad;
            _cameraMovementSpeed = cameraMovementSpeed;
            _cameraTurnRate = cameraTurnRate;
        }

        private void Update()
        {
            _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            SyncAngles();
        }

        private void FixedUpdate()
        {
            HandleCameraMovement();
        }

        private void HandleCameraMovement()
        {
            if (_cameraShouldMove)
            {
                transform.parent.position += _cameraMovementSpeed * Time.fixedDeltaTime * MoveVector;
            }
        }

        // Sync and update _alpha and _theta variables value
        private void SyncAngles()
        {
            _alpha = transform.parent.localEulerAngles.z * Mathf.Deg2Rad;
            _theta = transform.localEulerAngles.x * Mathf.Deg2Rad;
        }

        // Converts mouse position to map position
        private Vector3 MouseToMapPosition(Vector3 mousePosition)
        {
            // Symmetrical conversion of mouse position to game map position
            //which obtained from multiplying two matrix:
            //1.normal vector of 3 Plane which are involved with camera plane 3*3
            //2.camera normal vector 3*1
            // Dont change this part of code never ever without consciousness
            var gameMapPosX = mousePosition.x - (mousePosition.z * (Mathf.Sin(_alpha) * Mathf.Sin(_theta)) /
                                                 Mathf.Cos(_theta));
            var gameMapPosY = mousePosition.y + (mousePosition.z * (Mathf.Cos(_alpha) * Mathf.Sin(_theta)) /
                                                 Mathf.Cos(_theta));
            var gameMapPosZ = _mapPlane.position.z;
            return new Vector3(gameMapPosX, gameMapPosY, gameMapPosZ);
        }
    }
}