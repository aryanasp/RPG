using Model;
using UnityEngine;

namespace Controller
{
    public class FireSpellController : SpellController
    {
        //Spell Input Name
        private static readonly string Fire = "Fire";

        protected override bool ShouldInitializeAction(DebuffStatus debuffStatus)
        {
            return debuffStatus.DebuffName == "Fire Projectile";
        }
        
        protected override bool HandleInput()
        {
            return KeyController.AttackInputs[Fire];
        }

        
        //Execute debuff runs in every frame which is in debuff duration
        protected override void ExecuteDebuffs(GameObject damageDestination, DebuffStatus debuffStatus, float time)
        {
            DebuffMethods.DamagePerSecond(damageDestination.GetComponent<StatController>(),
                debuffStatus, ((FireSpellModel) Spell).BurningTicks, ((FireSpellModel) Spell).DamagePerTick, time, Spell.DebuffDuration);
        }
        
        
        //Animate debuff runs in every frame which is in debuff duration
        protected override void DebuffEffectGameObject(Transform animationTransform)
        {
            var spellEffect = Instantiate(((FireSpellModel) Spell).BurnCharacterPrefab, animationTransform.position,
                Quaternion.identity, animationTransform);
            Destroy(spellEffect, Spell.DebuffDuration);
        }

    }
}