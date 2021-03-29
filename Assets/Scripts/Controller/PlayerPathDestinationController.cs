using UnityEngine;

namespace Controller
{
    public class PlayerPathDestinationController : MonoBehaviour
    {
        
        private MovementController _parentMovementObject;
        // Start is called before the first frame update
        void Start()
        {
            _parentMovementObject = transform.parent.gameObject.GetComponent<MovementController>();
            transform.SetParent(null);
            Destroy(gameObject, 1f);
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = (Vector3) _parentMovementObject.TargetPoint;
        }
    }
}
