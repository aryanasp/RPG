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
            base.CastSpell();
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