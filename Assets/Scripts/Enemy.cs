using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;

    public Rigidbody2D target;
    
    public RuntimeAnimatorController[] anicon;


    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer sprite;
    Animator anime;
    WaitForFixedUpdate wait;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();

    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        if (!isLive || anime.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        if (!isLive)
            return;

        sprite.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        sprite.sortingOrder = 3;
        anime.SetBool("Dead", false);
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anime.runtimeAnimatorController = anicon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if(health > 0)
        {
            // 살았을때
            anime.SetTrigger("Hit");

        }
        else
        {
            // 죽었을때
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            sprite.sortingOrder = 2;
            anime.SetBool("Dead",true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
            
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; //하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
