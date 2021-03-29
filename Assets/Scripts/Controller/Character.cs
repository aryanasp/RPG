using UnityEngine;

namespace Controller
{
    public abstract class Character : MonoBehaviour
    {
        //Animator parameter variables
        private static readonly int Vx = Animator.StringToHash("vx");
        private static readonly int Vy = Animator.StringToHash("vy");
        protected static readonly int AnimatorAttack = Animator.StringToHash("attack");

        //Physics system variables
        [SerializeField] protected float moveSpeed;
    
        protected Vector2 MoveDirection { get; set; }

        protected Animator Animator { get; set; }

        private Rigidbody2D Rigidbody { get; set; }

        protected float DirectionAngle { get; set; }
    
        //States
        protected bool IsMoving => MoveDirection.SqrMagnitude() > 0.1f;

        protected bool IsAttacking
        {
            set;
            get;
        }
    
        //Attack
        protected Coroutine AttackCoroutine;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            InitializeGame();
        }

        void InitializeGame()
        {
            MoveDirection = Vector2.zero;
            DirectionAngle = -90;
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            FindStandingDirection();
            HandleLayers();
        }

        protected virtual void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Rigidbody.velocity = moveSpeed * MoveDirection.normalized;
        }

        void HandleLayers()
        {
            //Sync animator speed and character speed
            Animator.speed = moveSpeed;
            if (IsMoving)
            {
                ActivateLayer("WalkLayer");
                StopAttack();
                AnimateMovement();
            
            
            }
            else if (IsAttacking)
            {
                ActivateLayer("AttackLayer");
            }
            else
            {
                ActivateLayer("IdleLayer");
            }
        }
        
        protected void FindStandingDirection()
        {
            if (IsMoving)
            {
                DirectionAngle = Mathf.Atan(MoveDirection.y / MoveDirection.x) * Mathf.Rad2Deg;
                //Handling this ambiguation tan(180) = tan(0) = 0
                if (MoveDirection.x < 0 && DirectionAngle == 0)
                {
                    DirectionAngle = 180;
                }
            }
        }
        
        private void AnimateMovement()
        {
            //Sets the animation parameter so character animation walks in the correct direction
            Animator.SetFloat(Vx, MoveDirection.x);
            Animator.SetFloat(Vy, MoveDirection.y);
        }

        private void ActivateLayer(string layerName)
        {
            for (int i = 0; i < Animator.layerCount; i++)
            {
                Animator.SetLayerWeight(i, 0);
            }

            Animator.SetLayerWeight(Animator.GetLayerIndex(layerName), 1);
        }

        protected void StartAttack()
        {
            IsAttacking = true;
            Animator.SetBool(AnimatorAttack, IsAttacking);
        }
        protected void StopAttack()
        {
            if (AttackCoroutine != null)
            {
                StopCoroutine(AttackCoroutine);
                IsAttacking = false;
                Animator.SetBool(AnimatorAttack, IsAttacking);
            }
        
        }
    }
}