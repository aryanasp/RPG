using System;
using UnityEngine;

namespace Controller
{
    public class DamageController : MonoBehaviour
    {
        private StatController _statController;
        private bool _isTriggerEnter;

        public event Action<StatController> DealDamageAction; 
        // Start is called before the first frame update
        void Start()
        {
            _isTriggerEnter = false;
            _statController = GetComponent<StatController>();
        }

        // Update is called once per frame
        void Update()
        {
            //Don't move this to OnTriggerEnter2D because all components OnTriggerEnter2D runs before update in every unity lifecycle
            // but we need it to run in update(because SpellController updates)
            if (_isTriggerEnter)
            {
                DealDamageAction?.Invoke(_statController);
                _isTriggerEnter = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Hostile Projectile")
            {
                var damageSource = other.GetComponent<ProjectileController>().DamageSource;
                
                if (!damageSource.CompareTag(gameObject.tag))
                {
                    _isTriggerEnter = true;
                }
                else
                {
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
                }
            }
        }

        
    }
}
