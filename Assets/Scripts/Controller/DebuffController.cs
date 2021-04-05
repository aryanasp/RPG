using System;
using Model;
using UnityEngine;

namespace Controller
{
    public class DebuffController : MonoBehaviour
    {
        [SerializeField]
        private Transform underLegs;
        
        private float _timePassedFromFreeze;
        public event Action<GameObject, float, Vector2> DoDebuff;

        void Awake()
        {

        }
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void DebuffReset()
        {
            _timePassedFromFreeze = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            Debuff();
        }

        private void Debuff()
        {
            _timePassedFromFreeze += Time.deltaTime;
            DoDebuff?.Invoke(gameObject, _timePassedFromFreeze, underLegs.position);
        }
        
    }
}
