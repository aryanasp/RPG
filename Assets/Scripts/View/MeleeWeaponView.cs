using UnityEngine;

namespace View
{
    public interface IMeleeWeaponView : IWeaponView
    {
        
    }

    public class MeleeWeaponView : IMeleeWeaponView
    {
        private Collider2D _meleeWeaponCollider2D;


        public void Enter(params object[] list)
        {
            _meleeWeaponCollider2D = (Collider2D)list[0];
        }

        public void Attack(float directionAngle)
        {
            _meleeWeaponCollider2D.enabled = true;
            _meleeWeaponCollider2D.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, directionAngle);
        }

        public void Stop()
        {
            _meleeWeaponCollider2D.enabled = false;
        }
    }
}