using System;
using Model;
using UnityEngine;

namespace Controller
{
    public class FreezeDebuffController : MonoBehaviour
    {
        //TODO Duplicate danger
        private static readonly string FireProjectile = "Fire Projectile";
        private static readonly string IceProjectile = "Ice Projectile";
        
        [SerializeField] private FireSpellModel fireSpell;
        
        [SerializeField] private IceSpellModel iceSpell;

        [SerializeField]
        private Transform underLegs;

        private Vector2 _freezePosition;
        private bool _isFreeze = false;
        private float _timePassedFromFreeze;

        // Start is called before the first frame update
        void Start()
        {
            fireSpell.Initialize();
            iceSpell.Initialize();
            _timePassedFromFreeze = iceSpell.FreezeDebuffDuration;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Time.deltaTime * Vector2.right);
            _timePassedFromFreeze += Time.deltaTime;
            if (_timePassedFromFreeze >= iceSpell.FreezeDebuffDuration)
            {
                _isFreeze = false;
            }
            if (_isFreeze)
            {
                transform.position = _freezePosition;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(IceProjectile))
            {
                GameObject freezeEffect = Instantiate(iceSpell.FreezeAreaPrefab, underLegs.position, Quaternion.identity);
                _freezePosition = transform.position;
                _isFreeze = true;
                _timePassedFromFreeze = 0f;
            }
        }
    }
}
