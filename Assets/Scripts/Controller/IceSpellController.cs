using System;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Controller
{
    public class IceSpellController : SpellController
    {
        
        //Spell Input Name
        private static readonly string Ice = "Ice";
        private GameObject _freezeEffect;
        
        private List<ProjectileController> _projectileControllersList;
        private List<DebuffController> _debuffControllersList;
        private List<DebuffController> _initializedDebuffControllersList;
        DebuffModel _debuffMethods;
        
        protected override void Awake()
        {
            base.Awake();
            _freezeEffect = null;
            _projectileControllersList = new List<ProjectileController>();
            _debuffControllersList = new List<DebuffController>();
            _initializedDebuffControllersList = new List<DebuffController>();
            _debuffMethods = new DebuffModel();
        }

        protected override void Update()
        {
            base.Update();
            ExtractCollidedProjectiles();
            foreach (DebuffController debuffController in _debuffControllersList)
            {
                if (!_initializedDebuffControllersList.Contains(debuffController))
                {
                    debuffController.DoDebuff += Debuff;
                    _initializedDebuffControllersList.Add(debuffController);
                }
            }
        }

        private void ExtractCollidedProjectiles()
        {
            //projectiles list which should remove
            List<ProjectileController> removeProjectilesList = new List<ProjectileController>();
            //find collides which have collision with a game object
            foreach (ProjectileController projectile in _projectileControllersList)
            {
                var damageDestination = projectile.DamageDestination;
                //Add them to debuff controller to do some debuff on the debuff destination
                //Add the to remove projectiles list to removing from main list
                if (damageDestination != null)
                {
                    _debuffControllersList.Add(damageDestination.GetComponent<DebuffController>());
                    removeProjectilesList.Add(projectile);
                }
            }

            //remove projectiles which have collision with a game object from main list
            foreach (ProjectileController projectile in removeProjectilesList)
            {
                if (_projectileControllersList.Contains(projectile))
                {
                    _projectileControllersList.Remove(projectile);
                }
            }
        }

        protected override void CastSpell()
        {
            AttackTp = 0f;
            GameObject iceSpellGameObject = Instantiate(Spell.ProjectilePrefab, transform.position,
                Quaternion.Euler(0, 0, MovementController.DirectionAngle + 90), transform);
            _projectileControllersList.Add(iceSpellGameObject.GetComponent<ProjectileController>());
            StatController.ChangeStatValue(Mana, -Spell.ManaCost);
        }
        protected override bool HandleInput()
        {
            return KeyController.AttackInputs[Ice];
        }

        public override void Debuff(GameObject damageDestination,float time, Vector2 animationPosition)
        {
            ExecuteDebuffs(damageDestination, time);
            if (time >= ((IceSpellModel) Spell).FreezeDebuffDuration)
            {
                CancelDebuff(damageDestination);
            }
            else
            {
                AnimateDebuff(animationPosition);
            }
        }

        private void ExecuteDebuffs(GameObject damageDestination, float time)
        {
            _debuffMethods.Root(damageDestination.GetComponent<MovementController>(), ((IceSpellModel) Spell).FreezeDebuffDuration >= time);
        }
        
        private void AnimateDebuff(Vector2 animationPosition)
        {
            if (_freezeEffect == null)
            {
                _freezeEffect = Instantiate(((IceSpellModel) Spell).FreezeAreaPrefab, animationPosition,
                    Quaternion.identity);
                Destroy(_freezeEffect, ((IceSpellModel) Spell).FreezeDebuffDuration);
            }
        }

        private void CancelDebuff(GameObject damageDestination)
        {
            DebuffController debuffController = damageDestination.GetComponent<DebuffController>();
            if (_initializedDebuffControllersList.Contains(debuffController))
            {
                debuffController.DoDebuff -= Debuff;
                _initializedDebuffControllersList.Remove(debuffController);
            }
        }

        // public virtual void SpellOnTriggerEnter2D(Collider2D other)
        // {
        //     if (other.gameObject.CompareTag(IceProjectile))
        //     {
                    // GameObject freezeEffect = Instantiate(((IceSpellModel)Spell).FreezeAreaPrefab, underLegs.position, Quaternion.identity);
                    // _freezePosition = transform.position;
                    // _isFreeze = true;
                    // _timePassedFromFreeze = 0f;
        //     }
        // }
        
    }
}