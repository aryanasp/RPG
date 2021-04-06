using UnityEngine;

namespace Controller
{
    public class FireSpellController : SpellController
    {
        //Spell Input Name
        private static readonly string Fire = "Fire";
        
        
        
        protected override void CastSpell()
        {
            AttackTp = 0f;
            GameObject fireSpellGameObject = Instantiate(Spell.ProjectilePrefab, transform.position,
                Quaternion.Euler(0, 0, MovementController.DirectionAngle + 90), transform) as GameObject;
            StatController.ChangeStatValue(Mana, -Spell.ManaCost);
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