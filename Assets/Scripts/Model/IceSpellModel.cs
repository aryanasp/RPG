using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "Ice Spell Config")]
    public class IceSpellModel : SpellModel
    {
        [SerializeField] private GameObject freezeAreaPrefab;
        public GameObject FreezeAreaPrefab { get; private set; }
        
        public override void Initialize()
        {
            base.Initialize();
            FreezeAreaPrefab = freezeAreaPrefab;
        }
    }
}