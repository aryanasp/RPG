using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInputFromKeyboard();
        base.Update();
    }

    void GetInputFromKeyboard()
    {
        //Apply 0 movement speed in every starting of each frame
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
    }
    
}
