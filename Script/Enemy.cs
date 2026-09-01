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
    Collider2D enemy_collider;
    Animator enemy_animator;
    SpriteRenderer enemy_spriter;
    WaitForFixedUpdate wait;


    void Awake()
    {
        enemy_rigidbody = GetComponent<Rigidbody2D>();
        enemy_collider  = GetComponent<Collider2D>();
        enemy_animator = GetComponent<Animator>();
        enemy_spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }
    void FixedUpdate()
    {
        if (!isLive || enemy_animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//GetCurrentAnimatorStateInfo(Layerindex)<current state information
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
        enemy_collider.enabled = true;
        enemy_rigidbody.simulated = true;//enabled==simulated
        enemy_spriter.sortingOrder = 2;//request hardcoding
        enemy_animator.SetBool("Dead", false);
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
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if ( health >  0 )
        {
            // Live, hit action
            enemy_animator.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            enemy_collider.enabled = false;
            enemy_rigidbody.simulated = false;//enabled==simulated
            enemy_spriter.sortingOrder = 1;//request hardcoding
            enemy_animator.SetBool("Dead", true);
            //Dead();<Move to animator event
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;//Next one physics frame delay
        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 directtionVector = transform.position - playerPosition;
        enemy_rigidbody.AddForce(directtionVector.normalized * 3, ForceMode2D.Impulse);
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }
}
