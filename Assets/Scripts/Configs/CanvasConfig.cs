using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CanvasConfig", menuName = "CanvasConfig", order = 0)]
    public class CanvasConfig : ScriptableObject
    {
        [SerializeField] private GameObject canvasPrefab;

        public GameObject CanvasPrefab => canvasPrefab;
    }
}