using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Character : MonoBehaviour
{
    //Character physical stats
    [SerializeField] protected float moveSpeed;
    protected Vector2 MoveDirection { get; set; }

    //Animator parameter variables
    private Animator _animator;
    private static readonly int Vx = Animator.StringToHash("vx");
    private static readonly int Vy = Animator.StringToHash("vy");


    // Start is called before the first frame update
    protected virtual void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        MoveDirection = Vector2.zero;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Time.deltaTime * moveSpeed * MoveDirection);
        AnimateMovementAnimation();
    }

    private void AnimateMovementAnimation()
    {
        _animator.SetFloat(Vx, MoveDirection.x);
        _animator.SetFloat(Vy, MoveDirection.y);
    }
}