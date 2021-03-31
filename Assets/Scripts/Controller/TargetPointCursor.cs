using UnityEngine;

namespace Controller
{
    public class TargetPointCursor : MonoBehaviour
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
            if (_parentMovementObject.TargetPoint != (Vector2)_parentMovementObject.transform.position) //not displaying cursor in the character current position
            {
                transform.position = (Vector3) _parentMovementObject.TargetPoint;
            }
            
        }
    }
}
