using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "InputConfigs", menuName = "InputConfigs", order = 0)]
    public class InputConfigs : ScriptableObject
    {
        [SerializeField] private GameObject inputManagerPrefab;
        public GameObject InputManagerPrefab => inputManagerPrefab;
    }
}