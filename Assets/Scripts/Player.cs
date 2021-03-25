using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{   
    //Actions
    //Action which use to reduce the health
    public event Action<float> HealthReducer;
    //Action which use to increase the health
    public event Action<float> HealthRaiser;
    //Action which use to reduce the health
    public event Action<float> ManaReducer;
    //Action which use to increase the health
    public event Action<float> ManaRaiser;
    
    // Start is called before the first frame update
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
            HealthRaiser?.Invoke(10);
            ManaRaiser?.Invoke(30);
            
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            HealthReducer?.Invoke(10);
            ManaReducer?.Invoke(30);
        }
    }
    
}
