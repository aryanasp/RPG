using System.Collections;
using Model;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller
{
    public class SpellsController : MonoBehaviour
    {
        //Stat
        private static readonly string Health = "Health";
        private static readonly string Mana = "Mana";

        //Spells
        private static readonly string Fire = "Fire";

        private StatController _statController;

        //Player Movement
        private MovementController _movementController;

        //fire spell initialize variable
        //fire spell model
        [SerializeField]
        private SpellModel fireSpell;
        bool _canAttackFire;

        public bool CanAttackFire => _attackFireTp >= _attackFireCd;

        //Time passed from the last time we did fire attack
        private float _attackFireTp;

        //fire attack cool down time to attack again
        private float _attackFireCd;

        void Awake()
        {
            //fire spell initialize primary values for variables
            fireSpell.Initialize();
        }
        
        void Start()
        {
            _statController = GetComponent<StatController>();
            _movementController = GetComponent<MovementController>();
            
            
            _attackFireCd = fireSpell.CoolDown;
            _attackFireTp = _attackFireCd;
        }


        void Update()
        {
            CalculateLastAttackTimePassed();
        }

        private void CalculateLastAttackTimePassed()
        {
            _attackFireTp += Time.deltaTime;
        }

        public void CastSpell(string spell)
        {
            if (spell == Fire)
            {
                CastFireSpell();
            }
        }

        void CastFireSpell()
        {
            var transform1 = transform;

            _attackFireTp = 0f;
            GameObject fireSpellGameObject = Instantiate(fireSpell.ProjectilePrefab, transform1.position,
                Quaternion.Euler(0, 0, _movementController.DirectionAngle + 90)) as GameObject;
            _statController.ChangeStatValue(Mana, -fireSpell.ManaCost);
        }
    }
}