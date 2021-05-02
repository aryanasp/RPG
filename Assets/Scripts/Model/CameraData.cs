using System;
using UnityEngine;

namespace Model
{
    public class InitializeCameraEventArgs
    {
        public Transform MapPlane { set; get; }
        public float Alpha { set; get; }
        public float Theta { set; get; }
        public float CameraMovementSpeed { set; get; }
        public float CameraTurnRate { set; get; }
    }
    
    public interface ICameraData
    {
        event EventHandler<InitializeCameraEventArgs> OnInitializeCamera;
        Transform MapPlane { set; get; }
        float Alpha { set; get; }
        float Theta { set; get; }
        float CameraMovementSpeed { set; get; }
        float CameraTurnRate { set; get; }

        void InitializeCamera();

    }
    
    public class CameraData : ICameraData
    {
        public event EventHandler<InitializeCameraEventArgs> OnInitializeCamera = (sender, e) => {};
        public Transform MapPlane { get; set; }
        public float Alpha { get; set; }
        public float Theta { get; set; }
        public float CameraMovementSpeed { get; set; }
        public float CameraTurnRate { get; set; }
        
        
        public CameraData(Transform mapPlane, float alpha, float theta, float cameraMovementSpeed, float cameraTurnRate)
        {
            this.MapPlane = mapPlane;
            this.Alpha = alpha;
            this.Theta = theta;
            this.CameraMovementSpeed = cameraMovementSpeed;
            this.CameraTurnRate = cameraTurnRate;
        }
        
        public void InitializeCamera()
        {
            var eventArgs = new InitializeCameraEventArgs
            {
                MapPlane = MapPlane,
                Alpha = Alpha,
                Theta = Theta,
                CameraMovementSpeed = CameraMovementSpeed,
                CameraTurnRate = CameraTurnRate
            };
            OnInitializeCamera(this, eventArgs);
        }
        
    }
}