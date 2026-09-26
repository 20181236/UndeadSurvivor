using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D bullet_Rigidbody;

    void Awake()
    {
        bullet_Rigidbody= GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dirrection)
    {
        this.damage = damage; 
        this.per = per;

        if (per >= 0)
            bullet_Rigidbody.velocity = dirrection * 15f; // request fix hardcoding
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy") || per == -100)
            return;

        per--;

        if (per < 0)
        {
            bullet_Rigidbody.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;

        gameObject.SetActive(false);
    }
}
