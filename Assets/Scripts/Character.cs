using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed;

    protected Vector2 MoveDirection { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        MoveDirection = Vector2.zero; 
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }
    
    private void Move()
    {
        transform.Translate(Time.deltaTime * moveSpeed * MoveDirection );
    }
}
