using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace View
{

    public class CharacterAttackClickedEventArgs
    {
        
    }

    public class CharacterAttackFinishedEventArgs
    {
        
    }
    public interface ICharacterAttackView
    {
        event EventHandler<CharacterAttackClickedEventArgs> OnAttackClicked;
        event EventHandler<CharacterAttackFinishedEventArgs> OnAttackFinished;
        bool Attack { set; get; }
        IWeaponView Weapon { set; get; }
    }
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterAttackView : MonoBehaviour, ICharacterAttackView
    {
        private bool _attack;
        private Rigidbody2D _rigidbody2D;
        private IWeaponView _weapon;
        // Implement interface methods and properties
        public event EventHandler<CharacterAttackClickedEventArgs> OnAttackClicked = (sender, e) => { };
        public event EventHandler<CharacterAttackFinishedEventArgs> OnAttackFinished = (sender, e) => { };
        private float _attackCastTime = 1f;
        private float _timePassedFromLastAttack = 1000f;
        
        public bool Attack
        {
            get
            {
                return _attack;
            }
            set
            {
                _attack = value;
                _weapon.Enter(GetComponent<Collider2D>());
                _weapon.Attack(_rigidbody2D.angularDrag);
            }
        }

        public IWeaponView Weapon
        {
            get => _weapon;
            set
            {
                _weapon = value;
            }
        }

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            HandleInput();
            HandleTimePassedFromLastAttack();
        }

        private void HandleTimePassedFromLastAttack()
        {
            if (_timePassedFromLastAttack <= _attackCastTime)
            {
                _timePassedFromLastAttack += Time.deltaTime;
            }
            else
            {
                var eventArgs = new CharacterAttackFinishedEventArgs
                {
                    
                };
                OnAttackFinished(this, eventArgs);
            }
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (!Attack)
                {
                    Debug.Assert(Camera.main != null, "Camera.main != null");
                    var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    var eventArgs = new CharacterAttackClickedEventArgs
                    {
                        
                    };
                    OnAttackClicked(this, eventArgs);
                    _timePassedFromLastAttack = 0f;
                }
            }
        }

        
    }
}