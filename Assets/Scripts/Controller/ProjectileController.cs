using System;
using UnityEngine;

namespace Controller
{
    public class ProjectileController : MonoBehaviour
    {
        //TODO : How we should pass fire spell info to projectile controller ? here or SpellController : CastFireSpell
        [SerializeField] private float projectileSpeed;
        // Start is called before the first frame update

        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(projectileSpeed * Time.deltaTime * Vector2.down);
        }

        //TODO Handle player and enemy projectiles
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
            
        }
    }
}
