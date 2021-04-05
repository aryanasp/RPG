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

        //Next target point which is set from other movement components
        public Vector2 NextTargetPoint { get; set; }

        //Move point
        public Vector2 TargetPoint { get; private set; }


        //Attack Controller
        private AttackController _attackController;

        //is everything for moving
        private bool _canMove = true;

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
            _attackController = GetComponent<AttackController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            NextTargetPoint = Vector2.right;
            MoveDirection = Vector2.right;
            TargetPoint = new Vector2(0, 0);
            DirectionAngle = -90;
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            //Apply 0 movement speed in the starting of each frame
            MoveDirection = Vector2.zero;
            //Handle next target point
            TargetPoint = NextTargetPoint;
            //Determine character destination to go where
            FindVector2Destination();
            //Determine character direction
            FindStandingDirection();
            if (!_canMove)
            {
                StopWalk(true);
            }
        }

        void FixedUpdate()
        {
            Move();
        }

        void FindVector2Destination()
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

        public void StopWalk(bool condition, bool stopWithDestination = false)
        {
            //TODO root needs to change direction but not go in direction
            _canMove = !condition;
            MoveDirection = Vector2.zero;
            if (stopWithDestination)
            {
                TargetPoint = transform.position;
                NextTargetPoint = TargetPoint;
            }
            
        }
    }
}