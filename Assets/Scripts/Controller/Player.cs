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

        //Fight game objects
        [SerializeField] private GameObject projectilePrefab;
        
        void Awake()
        {
            _statController = GetComponent<StatController>();
            _keyController = GetComponent<KeyController>();
        }

        protected override void Start()
        {
            
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            HandleInput();
            base.Update();
        }

        // Change move direction to the point he should go
        
        
        void HandleInput()
        {
            //Handle player attacks
            if (_keyController.AttackInputs["Attack"])
            {
                if (!IsAttacking && !MovementController.IsMoving)
                {
                    AttackCoroutine = StartCoroutine(Attack());
                }
            }
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
                Quaternion.Euler(0, 0, MovementController.DirectionAngle + 90)) as GameObject;
        }
    }
}