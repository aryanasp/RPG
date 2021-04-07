using System;
using Controller;
using UnityEngine;

namespace Model
{
    public class DebuffMethodsModel
    {
        public void Root(MovementController movementController, bool rootCondition)
        {
            movementController.StopWalk(rootCondition);
        }

        public void DamagePerSecond(StatController statController, DebuffStatus debuffStatus,
            int ticks, float damagePerInterval, float time, float duration)
        {
            
            if (Mathf.Abs(time - (duration * (debuffStatus.SpecialStatus + 1) / ticks)) <= 0.1f)
            {
                
                debuffStatus.SpecialStatus += 1;
                statController.ChangeStatValue("Health", -damagePerInterval);
            }
            
        }
    }
}