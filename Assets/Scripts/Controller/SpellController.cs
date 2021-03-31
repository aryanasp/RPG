using System.Collections;
using Model;
using UnityEngine;

namespace Controller
{
    public abstract class SpellController : MonoBehaviour
    {
        //Stats
        protected static readonly string Health = "Health";
        protected static readonly string Mana = "Mana";
        protected StatController StatController{ get; private set; }

        //Movement
        protected MovementController MovementController { get; private set; }
        
        //Keys
        protected KeyController KeyController;
        
        //Attack
        protected AttackController AttackController;
        
        public bool IsCastingSpell { private set; get; }
        
        //Spell initialize variables
        //Spell model
        [SerializeField]
        protected SpellModel spell;

        //Time passed from the last time we did fire attack
        protected float AttackTp;

        //Spell cool down, Time which need to be able to attack again
        protected float AttackCd;
        
        //Spell cast time, Time elapse during spell is not fired but used
        protected float AttackCt;
        public bool CanAttack => (AttackTp >= AttackCd) && (IsResourcesEnough());

        //Attack coroutine
        private Coroutine AttackCoroutine { get; set; }

        protected virtual void Awake()
        {
            //fire spell initialize primary values for variables
            spell.Initialize();
            AttackController = GetComponent<AttackController>();
        }
        
        protected virtual void Start()
        {
            StatController = GetComponent<StatController>();
            MovementController = GetComponent<MovementController>();
            KeyController = GetComponent<KeyController>();
            AttackController.Spells.Add(this);
            AttackCt = spell.CastTime;
            AttackCd = spell.CoolDown;
            AttackTp = AttackCd;
        }

        private bool IsResourcesEnough()
        {
            return StatController.IsStatValueEnough(Health, spell.HealthCost) && StatController.IsStatValueEnough(Mana, spell.ManaCost);
        }
        
        private void Update()
        {
            HandleAttack();
            CalculateLastAttackTimePassed();
        }

        private void CalculateLastAttackTimePassed()
        {
            AttackTp += Time.deltaTime;
            AttackTp += Time.deltaTime;
        }

        protected abstract void CastSpell();
        
        void HandleAttack()
        {
            if (!IsCastingSpell && CanAttack)
            {
                if (HandleInput())
                {
                    AttackCoroutine = StartCoroutine(Attack());
                }
            }
        }

        protected abstract bool HandleInput();
        
        private IEnumerator Attack()
        {
            
            StartAttack();
            yield return new WaitForSeconds(AttackCt);
            CastSpell();
            StopAttack();
        }
        
        private void StartAttack()
        {
            MovementController.StopWalk();
            IsCastingSpell = true;
        }

        private void StopAttack()
        {
            if (AttackCoroutine != null)
            {
                StopCoroutine(AttackCoroutine);
                IsCastingSpell = false;
            }
        }
        
        


    }
}