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

    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] player_AnimatorController;

    void Awake()
    {
        player_Rigidbody = GetComponent<Rigidbody2D>();
        player_Spriter = GetComponent<SpriteRenderer>();
        player_Animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void OnEnable()
    {
        speed += Character.MoveSpeed;
        player_Animator.runtimeAnimatorController = player_AnimatorController[GameManager.instance.playerId];
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        //player_InputVector.x = Input.GetAxisRaw("Horizontal");
        //player_InputVector.y = Input.GetAxisRaw("Vertical");
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health < 0)
        {
            for (int index = 2; index < transform.childCount; index++)//2< hardcoding -> request naming 
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            player_Animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }

    }
}