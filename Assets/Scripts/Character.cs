using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Character : MonoBehaviour
{
    //Animator parameter variables
    private Animator _animator;
    private static readonly int Vx = Animator.StringToHash("vx");
    private static readonly int Vy = Animator.StringToHash("vy");
    
    //Physics system variables
    [SerializeField] protected float moveSpeed;
    protected Vector2 MoveDirection { get; set; }
    private Rigidbody2D _rigidbody;
    public bool IsMoving
    {
        get
        {
            return MoveDirection.SqrMagnitude() > 0.1f;
        }
    }
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        MoveDirection = Vector2.zero;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleLayers();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        _rigidbody.velocity = moveSpeed * MoveDirection.normalized;
    }

    void HandleLayers()
    {
        if (IsMoving)
        {
            AnimateMovement();
        }
        else
        {
            ActivateLayer("Idle Layer");
        }
    }

    private void AnimateMovement()
    {
        //Sync animator speed and character speed
        _animator.speed = moveSpeed;
        
        //Handle Walk and Idle animations
        if (IsMoving)
        {
            ActivateLayer("Walk Layer");
        }
        
        //Sets the animation parameter so character animation walks in the correct direction
        _animator.SetFloat(Vx, MoveDirection.x);
        _animator.SetFloat(Vy, MoveDirection.y);
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0);
        }
        _animator.SetLayerWeight(_animator.GetLayerIndex(layerName), 1);
    }
    
}