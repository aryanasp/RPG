using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class AttackController : MonoBehaviour
    {
        //Attack
        public bool IsAttacking { get; set; }
        
        //TODO inappropriate implementation
        public List<SpellController> Spells { get; set; }

        void Awake()
        {
            Spells = new List<SpellController>();
        }
        
        void Update()
        {
            if (Spells != null)
            {
                var attacks = false;
                foreach (SpellController sp in Spells)
                {
                    attacks = attacks || sp.IsCastingSpell;
                }
                IsAttacking = attacks;
            }
        }
        
    }
}