using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public float damage;
    public int per;
    Vector2 direction;

    GameManager player;
    Rigidbody2D rigid;

    void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;
        direction = dir;
        if (per >= 0)
        {
            rigid.linearVelocity = dir * 7.5f;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -10)
            return;

        per--;
        
        if (per <= 0)
        {
            rigid.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
        }
        else
        {
            Vector2 normal = (transform.position - collision.transform.position).normalized;
            direction = Vector2.Reflect(direction, normal).normalized;
            rigid.linearVelocity = direction * 7.5f;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if ( per == -10)
            return;

        if (collision.CompareTag("Screen"))
        {
            per--;

            if (per <= 0)
            {
                rigid.linearVelocity = Vector2.zero;
                gameObject.SetActive(false);
            }
            else
            {
                Vector2 normal = (transform.position - collision.transform.position).normalized;
                direction = Vector2.Reflect(direction, normal).normalized;
                rigid.linearVelocity = direction * 7.5f;
            } 
        }
        else if (collision.CompareTag("Area"))
        {
            gameObject.SetActive(false);
        }
        
    }
}
