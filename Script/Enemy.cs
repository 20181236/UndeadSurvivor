using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] enemy_animatorController;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D enemy_rigidbody;
    Animator enemy_animator;
    SpriteRenderer enemy_spriter;
    void Awake()
    {
        enemy_rigidbody = GetComponent<Rigidbody2D>();
        enemy_animator = GetComponent<Animator>();
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
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        enemy_animator.runtimeAnimatorController = enemy_animatorController[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if ( health >  0 )
        {
            // Live, hit action
        }
        else
        {
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
