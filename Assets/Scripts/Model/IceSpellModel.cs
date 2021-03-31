using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "Ice Spell Config")]
    public class IceSpellModel : SpellModel
    {
        [SerializeField] private GameObject freezeAreaPrefab;
        public GameObject FreezeAreaPrefab { get; private set; }
        
        [SerializeField] private float freezeDebuffDuration;
        public float FreezeDebuffDuration { get; private set; }
        public override void Initialize()
        {
            base.Initialize();
            FreezeAreaPrefab = freezeAreaPrefab;
            FreezeDebuffDuration = freezeDebuffDuration;
        }
    }
}