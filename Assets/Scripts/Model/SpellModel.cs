using UnityEngine;

namespace Model
{
    public abstract class SpellModel : ScriptableObject
    {
        [SerializeField] private float coolDown;
        [SerializeField] private float damage;
        [SerializeField] private float manaCost;
        [SerializeField] private float healthCost;
        [SerializeField] private GameObject projectilePrefab;
        public float CoolDown { get; protected set; }

        public float Damage { get; protected set; }

        public float ManaCost { get; protected set; }

        public float HealthCost { get; protected set; }
        
        public GameObject ProjectilePrefab { get; protected set; }

        public virtual void Initialize()
        {
            CoolDown = coolDown;
            Damage = damage;
            ManaCost = manaCost;
            HealthCost = healthCost;
            ProjectilePrefab = projectilePrefab;
        }
    }
}