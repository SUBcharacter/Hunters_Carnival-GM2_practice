using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefapId;
    public float damage;
    public float speed;
    public int count;

    int index;
    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
    }
    
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;
        }
    }

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        id = data.itemId;
        damage = data.baseDamage * Character.WeaponDamage;
        count = data.baseCount + Character.Count;

        for(int i=0;index < GameManager.instance.pool.Prefap.Length;i++)
        {
            if(data.projectile == GameManager.instance.pool.Prefap[i])
            {
                prefapId = i;
                break;
            }
        }

        switch(id)
        {
            case 0:
                speed = 300 * Character.WeaponSpeed;
                Inbound();
                break;
            
            default:
                speed = 0.5f * Character.WeaponRate;
                break;
        }

        // Hand unit
        Hand hand = player.hands[(int)data.itemType];
        hand.sprite.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void LevelUp(float damageUP, int countUP)
    {
        damage = damageUP * Character.WeaponDamage;
        count += countUP;

        if(id == 0)
        {
            Inbound();
        }
        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }

    void Inbound()
    {
        for(int i = 0; i <count; i++)
        {
            Transform bullet;

            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
               bullet = GameManager.instance.pool.Get(prefapId).transform;
               bullet.parent = transform;
            }
            
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -1 : ���� ����
        }
    }

    void Fire()
    {
        if(!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefapId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

        AudioManager.instance.PlaySFX(AudioManager.Sfx.Range);
    }
}

