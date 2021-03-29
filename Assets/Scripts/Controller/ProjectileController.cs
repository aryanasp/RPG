using UnityEngine;

namespace Controller
{
    public class ProjectileController : MonoBehaviour
    {
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
    }
}
