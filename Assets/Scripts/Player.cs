using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed;

    private Vector2 _playerMoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInputFromKeyboard();
        Move();
    }

    private void Move()
    {
        transform.Translate(Time.deltaTime * _playerSpeed * _playerMoveDirection );
    }

    void GetInputFromKeyboard()
    {
        //Apply 0 movement speed in every starting of each frame
        _playerMoveDirection = Vector2.zero;
        //Handle player movements
        if (Input.GetKey(KeyCode.W))
        {
            _playerMoveDirection += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _playerMoveDirection += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _playerMoveDirection += Vector2.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _playerMoveDirection += Vector2.left;
        }
    }
    
}
