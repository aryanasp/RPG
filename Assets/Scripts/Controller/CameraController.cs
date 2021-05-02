using Model;
using UnityEngine;
using View;

namespace Controller
{
    public interface ICameraController
    {
    }

    public class CameraController : ICameraController
    {
        private ICameraData _cameraData;
        private ICameraView _cameraView;

        public CameraController(ICameraData cameraData, ICameraView cameraView)
        {
            _cameraData = cameraData;
            _cameraView = cameraView;
            cameraView.OnCameraCreate += HandleCameraCreate;
            cameraData.OnInitializeCamera += HandleInitializeCamera;
        }

        private void HandleCameraCreate(object sender, CameraCreateEventArgs e)
        {
            _cameraData.InitializeCamera();
        }

        private void HandleInitializeCamera(object sender, InitializeCameraEventArgs e)
        {
            _cameraView.InitializeCamera(e.MapPlane, e.Alpha, e.Theta, e.CameraMovementSpeed, e.CameraTurnRate);
        }
    }
}