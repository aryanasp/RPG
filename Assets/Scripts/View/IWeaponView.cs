using UnityEngine;

namespace View
{
    public interface IWeaponView
    {
        void Enter(params object[] list);
        void Attack(float directionAngle);
        void Stop();
    }
}
