using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Controller
{
    public abstract class SpellController : MonoBehaviour
    {
        //Stats
        protected const string Health = "Health";
        protected const string Mana = "Mana";
        protected StatController StatController { get; private set; }

        //Movement
        protected MovementController MovementController { get; private set; }

        //Keys
        protected KeyController KeyController;

        //Attack
        protected AttackController AttackController;


        public bool IsCastingSpell { private set; get; }

        //Spell initialize variables
        //Spell model
        [SerializeField] private SpellModel spell;

        public SpellModel Spell
        {
            get { return spell; }
            protected set { spell = value; }
        }

        //Time passed from the last time we did fire attack
        protected float AttackTp;

        //Spell cool down, Time which need to be able to attack again
        protected float AttackCd;

        //Spell cast time, Time elapse during spell is not fired but used
        protected float AttackCt;
        public bool CanAttack => (AttackTp >= AttackCd) && (IsResourcesEnough());

        //Extracting debuff controllers lists
        //Spell projectiles list which is leased from spell caster
        protected List<ProjectileController> ProjectileControllersList;
        protected List<DebuffController> DebuffControllersList;
        protected List<DebuffController> InitializedDebuffControllersList;

        // protected GameObject SpellEffectGameObject;

        protected DebuffMethodsModel DebuffMethods;

        //Attack coroutine
        private Coroutine AttackCoroutine { get; set; }
        
        protected virtual void Awake()
        {
            //fire spell initialize primary values for variables
            spell.Initialize();
            AttackController = GetComponent<AttackController>();
            //Extracting debuff controllers lists initialization
            ProjectileControllersList = new List<ProjectileController>();
            DebuffControllersList = new List<DebuffController>();
            InitializedDebuffControllersList = new List<DebuffController>();
            DebuffMethods = new DebuffMethodsModel();
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
            return StatController.IsStatValueEnough(Health, spell.HealthCost) &&
                   StatController.IsStatValueEnough(Mana, spell.ManaCost);
        }

        protected virtual void Update()
        {
            HandleAttack();
            CalculateLastAttackTimePassed();
            ExtractDamageReceiversDebuffControllers();
        }


        private void ExtractDamageReceiversDebuffControllers()
        {
            //Projectiles list which should remove from main list
            List<ProjectileController> removeProjectilesList = new List<ProjectileController>();
            //Find collides which have collision with a game object
            foreach (ProjectileController projectile in ProjectileControllersList)
            {
                var damageDestination = projectile.DamageDestination;
                //Add them to debuff controller to do some debuff on the debuff destination
                //Add the to remove projectiles list to removing from main list
                if (damageDestination != null)
                {
                    DebuffControllersList.Add(damageDestination.GetComponent<DebuffController>());
                    removeProjectilesList.Add(projectile);
                }
            }

            //Remove projectiles which have collision with a game object from main list
            foreach (ProjectileController projectile in removeProjectilesList)
            {
                if (ProjectileControllersList.Contains(projectile)
                ) //maybe game object destroyed in this time so we check 
                {
                    ProjectileControllersList.Remove(projectile);
                }
            }

            //Find damage receivers debuff controllers
            foreach (DebuffController debuffController in DebuffControllersList)
            {
                if (!InitializedDebuffControllersList.Contains(debuffController))
                {
                    debuffController.DoDebuff += Debuff;
                    InitializedDebuffControllersList.Add(debuffController);
                }
            }

            //Remove damage receivers from debuff controllers which are initialized
            foreach (DebuffController debuffController in InitializedDebuffControllersList)
            {
                if (InitializedDebuffControllersList.Contains(debuffController))
                {
                    DebuffControllersList.Remove(debuffController);
                }
            }
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
            MovementController.StopWalk(true, true);
            IsCastingSpell = true;
        }

        private void StopAttack()
        {
            MovementController.StopWalk(false);
            if (AttackCoroutine != null)
            {
                StopCoroutine(AttackCoroutine);
                IsCastingSpell = false;
            }
        }

        private void Debuff(GameObject damageDestination, int slotID, Vector2 animationPosition)
        {
            //Debuff status for current debuff
            var debuffStatus = damageDestination.GetComponent<DebuffController>().DebuffsStatusList[slotID];
            ExecuteDebuffs(damageDestination, debuffStatus.TimePassedFromDebuff);
            if (debuffStatus.TimePassedFromDebuff < Spell.DebuffDuration)
            {
                
                //TODO animation things should go in behaviour components
                if (debuffStatus.IsInDebuff)
                {
                    
                    DebuffEffectGameObject(animationPosition);
                    debuffStatus.IsInDebuff = false;
                }
            }
            else
            {
                //Cancelling debuff
                CancelDebuff(damageDestination, slotID);
            }
        }

        protected virtual void CancelDebuff(GameObject damageDestination, int slotId)
        {
            var debuffController = damageDestination.GetComponent<DebuffController>();
            var debuffStatus = debuffController.DebuffsStatusList[slotId];
            if (InitializedDebuffControllersList.Contains(debuffController))
            {
                InitializedDebuffControllersList.Remove(debuffController);
                debuffController.DoDebuff -= Debuff;
                debuffStatus.FreeDebuffSlot();
            }
        }

        protected abstract GameObject DebuffEffectGameObject(Vector2 animationPosition);


        protected abstract void ExecuteDebuffs(GameObject damageDestination, float time);
    }
}