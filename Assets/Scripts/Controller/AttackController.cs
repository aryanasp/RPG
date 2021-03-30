using System.Collections;
using UnityEngine;

namespace Controller
{
    public class AttackController : MonoBehaviour
    {
       
        private MovementController _movementController;
        
        //Keys
        private KeyController _keyController;
        
        //Fight game objects
        [SerializeField] private GameObject projectilePrefab;
        
        public bool IsAttacking
        {
            private set;
            get;
        }
        
        //Attack
        private Coroutine _attackCoroutine;
        
        // Start is called before the first frame update
        void Start()
        {
            _keyController = GetComponent<KeyController>();
            _movementController = GetComponent<MovementController>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }
        
        void HandleInput()
        {
            //Handle player attacks
            if (_keyController.AttackInputs["Attack"])
            {
                if (!IsAttacking)
                {
                    _movementController.StopWalk();
                    _attackCoroutine = StartCoroutine(Attack());
                }
            }
        }
        
        private IEnumerator Attack()
        {
            
            StartAttack();
            yield return new WaitForSeconds(2);
            CastSpell();
            StopAttack();
        }

        
        private void StartAttack()
        {
            IsAttacking = true;
        }

        
        public void StopAttack()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                IsAttacking = false;
            }
        }
        
        private void CastSpell()
        {
            var transform1 = transform;
            GameObject fireSpell = Instantiate(projectilePrefab, transform1.position,
                Quaternion.Euler(0, 0, _movementController.DirectionAngle + 90)) as GameObject;
        }
        
    }
}
