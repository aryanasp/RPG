using UnityEngine;

namespace Controller
{
    public class PlayerInputController : MonoBehaviour
    {
        //Keys management
        private KeyController _keyController; 
        
        //Movement controller
        private MovementController _movementController;
        
        //Attack controller
        private AttackController _attackController;
        
        //image which show up when u determine destination
        [SerializeField] private GameObject targetPointGameObject; 
        private GameObject _pointGameObject; 
        
        void Awake()
        {
            _keyController = GetComponent<KeyController>(); 
            _attackController = GetComponent<AttackController>();
            _movementController = GetComponent<MovementController>();
        }
        
        void Update()
        {
            HandleInput();
        }
  
        void HandleInput()
        {
            if (_keyController.MovementInputs["Walk"])
            {
                if (!_attackController.IsAttacking)
                {
                    //Animate player destination point cursor
                    TargetPointCursor();
                    //Find player path target point
                    _movementController.NextTargetPoint = new Vector2(_keyController.MousePositions["X"], _keyController.MousePositions["Y"]);
                }
                
            }
        }
        
        private void TargetPointCursor()
        {
            if (!_pointGameObject)
            {
                _pointGameObject = Instantiate(targetPointGameObject, _movementController.TargetPoint, Quaternion.Euler(65, 0, 0), transform) as GameObject;
            }
        }
    }
}