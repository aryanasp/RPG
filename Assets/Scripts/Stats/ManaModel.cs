using System;
using UnityEngine;

namespace Stats
{
    public class ManaModel : StatModel
    {
        
        public override void Enter(float currentStatValue, float maxStatValue)
        {
            StatBarName = "Mana";
            base.Enter(currentStatValue, maxStatValue);
        }
    }
}
