using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer sprite;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.188f, -0.15f, 0);
    Quaternion LeftRot = Quaternion.Euler(0, 0, -27);
    Quaternion LeftRotReverse = Quaternion.Euler(0, 0, -153);

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate()
    {
        bool isReverse = player.flipX;

        if(isLeft) // 근접무기
        {
            transform.localRotation = isReverse ? LeftRotReverse : LeftRot;
            sprite.flipY = isReverse;
            sprite.sortingOrder = isReverse ? 4 : 6;
        }
        else // 원거리
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            sprite.flipX = isReverse;
            sprite.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
