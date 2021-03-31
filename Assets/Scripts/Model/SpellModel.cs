using UnityEngine;

namespace Model
{
    public abstract class SpellModel : ScriptableObject
    {
        [SerializeField] private float coolDown;
        [SerializeField] private float castTime;
        [SerializeField] private float damage;
        [SerializeField] private float manaCost;
        [SerializeField] private float healthCost;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private GameObject projectilePrefab;
        public float CoolDown { get; protected set; }
        
        public float CastTime { get; protected set; }

        public float Damage { get; protected set; }

        public float ManaCost { get; protected set; }

        public float HealthCost { get; protected set; }
        
        public GameObject ProjectilePrefab { get; protected set; }
        
        public float ProjectileSpeed { get; protected set; }

        public virtual void Initialize()
        {
            CoolDown = coolDown;
            CastTime = castTime;
            Damage = damage;
            ManaCost = manaCost;
            HealthCost = healthCost;
            projectileSpeed = ProjectileSpeed;
            ProjectilePrefab = projectilePrefab;
        }
    }
}