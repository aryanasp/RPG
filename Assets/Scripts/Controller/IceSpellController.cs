using UnityEngine;

namespace Controller
{
    public class IceSpellController : SpellController
    {
        //Spell Input Name
        private static readonly string Ice = "Ice";
        

        protected override void CastSpell()
        {
            var transform1 = transform;
            AttackTp = 0f;
            GameObject iceSpellGameObject = Instantiate(spell.ProjectilePrefab, transform1.position,
                Quaternion.Euler(0, 0, MovementController.DirectionAngle + 90)) as GameObject;
            StatController.ChangeStatValue(Mana, -spell.ManaCost);
        }
        protected override bool HandleInput()
        {
            
            return KeyController.AttackInputs[Ice];
        }

        public void Debuff()
        {
            
        }
    }
}