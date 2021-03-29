using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;


public class Player : Character
{
    //Animator variables
    //Stat
    private StatController _statController;
    private static readonly string Health = "Health";
    private static readonly string Mana = "Mana";
    

    void Awake()
    {
        _statController = GetComponent<StatController>();
    }
    
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInputFromKeyboard();
        base.Update();
    }

    void GetInputFromKeyboard()
    {
        //Apply 0 movement speed in the starting of each frame
        MoveDirection = Vector2.zero;
        //Handle player movements
        if (Input.GetKey(KeyCode.W))
        {
            MoveDirection += Vector2.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            MoveDirection += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveDirection += Vector2.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            MoveDirection += Vector2.left;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackCoroutine = StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            _statController.ChangeStatValue(Health, 10);
            _statController.ChangeStatValue(Mana, 40);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            _statController.ChangeStatValue(Health, -10);
            _statController.ChangeStatValue(Mana, -40);
        }
    }

    private IEnumerator Attack()
    {
        if (IsAttacking || IsMoving) yield break;
        StartAttack();  
        yield return new WaitForSeconds(1);
        StopAttack();
    } 
}