using UnityEngine;

namespace Controller
{
    public class FireSpellController : SpellController
    {
        //Spell Input Name
        private static readonly string Fire = "Fire";
        
        
        
        protected override void CastSpell()
        {
            base.CastSpell();
        }

        protected override bool HandleInput()
        {
            return KeyController.AttackInputs[Fire];
        }

        protected override GameObject DebuffEffectGameObject(Vector2 animationPosition)
        {
            return null;
        }

        protected override void ExecuteDebuffs(GameObject damageDestination, float time)
        {
            
        }
    }
}