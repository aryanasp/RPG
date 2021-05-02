using System.Collections.Generic;
using Configs;
using Controller;
using Model;
using UnityEngine;
using View;

namespace Initializers
{
    public interface ICameraInitializer
    {
    }

    public class CameraInitializer : ICameraInitializer
    {
        public CameraInitializer(CameraConfig cameraConfig, Vector3 position, Vector3 rotation)
        {
            // Create camera
            GameObject cameraGameObject =
                UnityEngine.Object.Instantiate(cameraConfig.CameraPrefab, position, Quaternion.identity);
            // Initialize camera data
            // Get camera initial data from config object
            var cameraData = new CameraData(cameraConfig.MapPlanTransform, cameraConfig.AlphaPivotAngleWithPlane,
                cameraConfig.ThetaCameraAngleWithPivot,
                cameraConfig.CameraMovementSpeed, cameraConfig.CameraTurnRate);
            // Initialize camera view
            var cameraView = cameraGameObject.GetComponentInChildren<ICameraView>();
            // Initialize camera controller
            var cameraController = new CameraController(cameraData, cameraView);
            
            // Create camera boundaries
            var cameraBoundariesViews = cameraGameObject.GetComponentsInChildren<IBoundariesView>();
            var cameraBoundariesControllers = new List<CameraBoundariesController>();
            foreach (var cameraBoundariesView in cameraBoundariesViews)
            {
                cameraBoundariesControllers.Add(new CameraBoundariesController(cameraView, cameraBoundariesView));
            }
        }
    }
}