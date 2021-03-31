using System.Collections;
using UnityEngine;

namespace Controller
{
    public class AttackController : MonoBehaviour
    {
       
        //variables
        private static readonly string Fire = "Fire";
        
        private MovementController _movementController;
        
        //Keys
        private KeyController _keyController;

        private SpellsController _spellsController;
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
            _spellsController = GetComponent<SpellsController>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }
        
        void HandleInput()
        {
            if (!IsAttacking)
            {
                //Handle player fire spell
                if (_keyController.AttackInputs[Fire])
                {
                    
                    _attackCoroutine = StartCoroutine(Attack(Fire));

                }
            }
        }
        
        private IEnumerator Attack(string spell)
        {
            if (_spellsController.CanAttackFire)
            {
                _movementController.StopWalk();
                StartAttack();
                yield return new WaitForSeconds(2);
                _spellsController.CastSpell(spell);
                StopAttack();
            }
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
        
    }
}
