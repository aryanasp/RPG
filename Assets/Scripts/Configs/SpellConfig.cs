using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Configs
{
    [CustomEditor(typeof(SpellConfig))]
    public class MyScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var myScript = target as SpellConfig;

            Debug.Assert(myScript != null, nameof(myScript) + " != null");
            if (myScript.IsProjectile)
                myScript.ProjectileSpeed =
                    EditorGUILayout.Slider("Projectile Speed ", myScript.ProjectileSpeed, 0, 100);
            
            if (myScript.IsArea)
                myScript.AreaRadius =
                    EditorGUILayout.Slider("Area Radius ", myScript.AreaRadius, 0, 10);
            
            if (myScript.HasDuration)
                myScript.SpellDuration =
                    EditorGUILayout.Slider("Spell Duration ", myScript.SpellDuration, 0, 20);
            
        }
    }

    [CreateAssetMenu(fileName = "SpellConfig", menuName = "SpellConfig", order = 0)]
    public class SpellConfig : ScriptableObject
    {
        [Header("Spell Name", order = 1)] 
        [SerializeField] private string spellName;
        public string SpellName => spellName;
        [Header("Spell Prefab", order = 1)] 
        [SerializeField] private GameObject spellPrefab;
        public GameObject SpellPrefab => spellPrefab;
        [Space]
        [Space]
        [Header("Spell Attributes", order = 1)] 
        [SerializeField] private float damage;
        public float Damage => damage;
        
        [SerializeField] private float spellCoolDown;
        public float SpellCoolDown => spellCoolDown;
        
        [SerializeField] private float spellCastTime;
        public float SpellCastTime => spellCastTime;
        
        [SerializeField] private bool isProjectile;
        public bool IsProjectile
        {
            get => isProjectile;
            set => isProjectile = value;
        }

        private float _projectileSpeed;

        public float ProjectileSpeed
        {
            get => _projectileSpeed;
            set => _projectileSpeed = value;
        }
        
        
        [SerializeField] private bool isArea;
        public bool IsArea
        {
            get => isArea;
            set => isArea = value;
        }
        
        private float _areaRadius;

        public float AreaRadius
        {
            get => _areaRadius;
            set => _areaRadius = value;
        }
        
        [SerializeField] private bool hasDuration;
        public bool HasDuration
        {
            get => hasDuration;
            set => hasDuration = value;
        }

        private float _spellDuration;

        public float SpellDuration
        {
            get => _spellDuration;
            set => _spellDuration = value;
        }
    }
}