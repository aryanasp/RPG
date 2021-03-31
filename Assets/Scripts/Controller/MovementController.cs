using UnityEngine;

namespace Controller
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        public float MoveSpeed => moveSpeed;
        private Rigidbody2D Rigidbody { get; set; }
        public float DirectionAngle { get; private set; }
        public Vector2 MoveDirection { get; private set; }
        //States
        public bool IsMoving => MoveDirection.SqrMagnitude() > 0.1f;
        
        //Keys management
        private KeyController _keyController;
        
        //Move point

        public Vector2 TargetPoint { get; private set; }

        //image which show up when u determine destination
        [SerializeField] private GameObject targetPointGameObject;
        private GameObject _pointGameObject;
        
        //Attack Controller
        private AttackController _attackController;
        
        private bool IsInPoint
        {
            get
            {
                var position = transform.position;
                return Mathf.Abs(TargetPoint.x - position.x) < 0.1f &&
                       Mathf.Abs(TargetPoint.y - position.y) < 0.1f;
            }
        }

        void Awake()
        {
            _keyController = GetComponent<KeyController>();
            _attackController = GetComponent < AttackController>();
        }
        
        // Start is called before the first frame update
        void Start()
        {
            MoveDirection = Vector2.zero;
            TargetPoint = new Vector2(0, 0);
            DirectionAngle = -90;
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            //Apply 0 movement speed in the starting of each frame
            MoveDirection = Vector2.zero;
            //Handle inputs
            HandleInput();
            //Determine player destination to go where
            Go2Point();
            //Determine player direction
            FindStandingDirection();
            
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
                    TargetPoint = new Vector2(_keyController.MousePositions["X"], _keyController.MousePositions["Y"]);
                }
                
            }
        }

        void FixedUpdate()
        {
            Move();
        }
        
        void Go2Point()
        {
            if (!_attackController.IsAttacking)
            {
                MoveDirection = !IsInPoint ? (TargetPoint - (Vector2) transform.position).normalized : Vector2.zero;
            }

        }
        
        private void Move()
        {
            Rigidbody.velocity = MoveSpeed * MoveDirection.normalized;
        }

        private void FindStandingDirection()
        {
            if (IsMoving)
            {
                DirectionAngle = Mathf.Atan(MoveDirection.y / MoveDirection.x) * Mathf.Rad2Deg;
                //Handling this ambiguity tan(180) = tan(0) = 0
                if (MoveDirection.x < 0)
                {
                    DirectionAngle += 180;
                }
            }
        }
        
        private void TargetPointCursor()
        {
            if (!_pointGameObject)
            {
                _pointGameObject = Instantiate(targetPointGameObject, TargetPoint, Quaternion.Euler(65, 0, 0), transform) as GameObject;
            }
            
        }

        public void StopWalk()
        {
            MoveDirection = Vector2.zero;
            TargetPoint = transform.position;
        }
        
    }
}
