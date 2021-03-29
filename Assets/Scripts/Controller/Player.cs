using System.Collections;
using UnityEngine;

namespace Controller
{
    public class Player : Character
    {
        //Animator variables
        //Stat
        private static readonly string Health = "Health";
        private static readonly string Mana = "Mana";

        private StatController _statController;

        //Keys
        private KeyController _keyController;

        //Move point
        private Vector2 _pointPosition;
        
        //Mouse set point position image
        [SerializeField] private GameObject goalGameObject;
        private GameObject _pointGameObject;
        
        private bool IsInPoint
        {
            get
            {
                var position = transform.position;
                return Mathf.Abs(_pointPosition.x - position.x) < 0.01f &&
                       Mathf.Abs(_pointPosition.y - position.y) < 0.01f;
            }
        }
            
        //Fight game objects
        [SerializeField] private GameObject target;
        [SerializeField] private GameObject projectilePrefab;


        void Awake()
        {
            _statController = GetComponent<StatController>();
            _keyController = GetComponent<KeyController>();
        }

        protected override void Start()
        {
            _pointPosition = new Vector2(0, 0);
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            HandleInput();
            Go2Point();
            base.Update();
        }

        // Change move direction to the point he should go
        void Go2Point()
        {
            MoveDirection = !IsInPoint ? (_pointPosition - (Vector2) transform.position).normalized : Vector2.zero;
        }
        
        void HandleInput()
        {
            //Apply 0 movement speed in the starting of each frame
            MoveDirection = Vector2.zero;
            //Handle player movements
            if (_keyController.MovementInputs["Walk"])
            {
                //Animate player destination point cursor
                HandleTargetPointAnimation();
                //Determine player destination to go where
                _pointPosition = new Vector2(_keyController.MousePositions["X"], _keyController.MousePositions["Y"]);
            }
            //Handle player attacks
            if (_keyController.AttackInputs["Attack"])
            {
                if (!IsAttacking && !IsMoving)
                {
                    AttackCoroutine = StartCoroutine(Attack());
                }
            }
        }

        private void HandleTargetPointAnimation()
        {
            if (!_pointGameObject)
            {
                _pointGameObject = Instantiate(goalGameObject, _pointPosition, Quaternion.Euler(65, 0, 0)) as GameObject;
            }

            _pointGameObject.transform.position = _pointPosition;
            Destroy(_pointGameObject, 1f);
        }


        private IEnumerator Attack()
        {
            StartAttack();
            yield return new WaitForSeconds(1);
            CastSpell();
            StopAttack();
        }

        private void CastSpell()
        {
            var transform1 = transform;
            GameObject fireSpell = Instantiate(projectilePrefab, transform1.position,
                transform1.rotation * Quaternion.Euler(0, 0, DirectionAngle + 90)) as GameObject;
        }
    }
}