using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "CameraConfig", order = 0)]
    public class CameraConfig : ScriptableObject
    {
        [Header("Prefab")] 
        [SerializeField] private GameObject cameraPrefab;
        public GameObject CameraPrefab => cameraPrefab;
        [Header("Transform")]
        [SerializeField] private GameObject mapPlane;
        public Transform MapPlanTransform => mapPlane.transform;
        [SerializeField] private float alphaPivotAngleWithPlane;
        public float AlphaPivotAngleWithPlane => alphaPivotAngleWithPlane;
        [SerializeField] private float thetaCameraAngleWithPivot;
        public float ThetaCameraAngleWithPivot => thetaCameraAngleWithPivot;
        [Header("Physics")]
        [SerializeField] private float cameraMovementSpeed;
        public float CameraMovementSpeed => cameraMovementSpeed;
        [SerializeField] private float cameraTurnRate;
        public float CameraTurnRate => cameraTurnRate;

        
    }
}