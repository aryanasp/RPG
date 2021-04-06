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
        
        protected override void CastSpell()
        {
            AttackTp = 0f;
            GameObject iceSpellGameObject = Instantiate(Spell.ProjectilePrefab, transform.position,
                Quaternion.Euler(0, 0, MovementController.DirectionAngle + 90), transform);
            ProjectileControllersList.Add(iceSpellGameObject.GetComponent<ProjectileController>());
            StatController.ChangeStatValue(Mana, -Spell.ManaCost);
        }

        protected override bool HandleInput()
        {
            return KeyController.AttackInputs[Ice];
        }

        //Execute debuff runs in every frame which is in debuff duration
        protected override void ExecuteDebuffs(GameObject damageDestination, float time)
        {
            DebuffMethods.Root(damageDestination.GetComponent<MovementController>(),
                Spell.DebuffDuration >= time);
        }

        //Animate debuff runs in every frame which is in debuff duration
        protected override GameObject DebuffEffectGameObject(Vector2 animationPosition)
        {
            var spellEffect = Instantiate(((IceSpellModel) Spell).FreezeAreaPrefab, animationPosition,
                Quaternion.identity);
            Destroy(spellEffect, Spell.DebuffDuration);
            return spellEffect;
        }
    }
}