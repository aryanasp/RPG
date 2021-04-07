using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "Fire Spell Config")]
    public class FireSpellModel : SpellModel
    {
        
        [SerializeField] private int burningTicks;
        public int BurningTicks { get; private set; }

        
        [SerializeField] private float damagePerTick;
        public float DamagePerTick { get; private set; }
        
        [SerializeField] private GameObject burnCharacterPrefab;
        public GameObject BurnCharacterPrefab { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            BurningTicks = burningTicks;
            DamagePerTick = damagePerTick;
            BurnCharacterPrefab = burnCharacterPrefab;
        }
    }
}
