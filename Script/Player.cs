using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 player_InputVector;
    public float speed;

    Rigidbody2D player_Rigidbody;
    SpriteRenderer player_Spriter;
    Animator player_Animator;
    void Start()
    {
        player_Rigidbody = GetComponent<Rigidbody2D>();
        player_Spriter = GetComponent<SpriteRenderer>();
        player_Animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        ////1. Addfoce
        //player_Rigidbody.AddForce(player_InputVector);
        ////2. Velocity
        //player_Rigidbody.velocity = player_InputVector;
        //3. MovePosition
        Vector2 nextVector = player_InputVector/*.normalized*/ * speed * Time.fixedDeltaTime;
        player_Rigidbody.MovePosition(player_Rigidbody.position + nextVector);
    }

    void LateUpdate()
    {
        player_Animator.SetFloat("Speed", player_InputVector.magnitude);

        if (player_InputVector.x != 0)
        {
            player_Spriter.flipX = player_InputVector.x < 0;
        }
    }

    void OnMove(InputValue value)
    {
        player_InputVector = value.Get<Vector2>();
    }


}
