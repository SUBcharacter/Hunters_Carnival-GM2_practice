using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;

    public Rigidbody2D target;
    Animator anime;
    public RuntimeAnimatorController[] anicon;

    bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer sprite;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        sprite.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        isLive = true;
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
}
