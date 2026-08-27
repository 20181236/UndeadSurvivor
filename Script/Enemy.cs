using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D enemy_rigidbody;
    SpriteRenderer enemy_spriter;
    void Awake()
    {
        enemy_rigidbody = GetComponent<Rigidbody2D>();
        enemy_spriter = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (!isLive)
            return;
         
        Vector2 directionVector = target.position - enemy_rigidbody.position;
        Vector2 nextVector = directionVector.normalized * speed * Time.fixedDeltaTime;
        enemy_rigidbody.MovePosition(enemy_rigidbody.position + nextVector);
        enemy_rigidbody.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        enemy_spriter.flipX = target.position.x < enemy_rigidbody.position.x;
    }

    void OnEnable()
    {
        target = GameManager.gameManagerInstance.player.GetComponent<Rigidbody2D>();
    }
}
