using System.Collections.Generic;
using Stats;
using UnityEngine;


public class Player : Character
{

    //Stat
    private StatsHandler _statsHandler;
    private static readonly string Health = "Health";
    private static readonly string Mana = "Mana";
    void Awake()
    {
        _statsHandler = GetComponent<StatsHandler>();
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


        //Debug Stat Bar
        if (Input.GetKeyDown(KeyCode.I))
        {
            _statsHandler.IncreaseStatValue(Health, 10);
            _statsHandler.IncreaseStatValue(Mana, 50);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            _statsHandler.DecreaseStatValue(Health, 10);
            _statsHandler.DecreaseStatValue(Mana, 50);
        }
    }
}