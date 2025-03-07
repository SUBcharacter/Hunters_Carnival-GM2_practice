using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public WeaponData[] weaponData;

    int index = 0;
    float timer;
    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    void Start()
    {
        Init();
    }
    void Update()
    {
        switch (weaponData[index].id)
        {
            case 0:
                transform.Rotate(Vector3.back * weaponData[0].speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if (timer > weaponData[index].speed)
                {
                    timer = 0;
                    Fire();
                }
                break;
        }

        // 테스트
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(4,1);
        }
    }

    public void Init()
    {
        switch(weaponData[index].id)
        {
            case 0:
                weaponData[index].speed = 300;
                Inbound();
                break;
            
            default:
                weaponData[index].speed = 0.1f;
                break;
        }
    }

    public void LevelUp(float damage, int count)
    {
        weaponData[index].damage += damage;
        weaponData[index].count += count;

        if(weaponData[index].id == 0)
        {
            Inbound();
        }
    }

    void Inbound()
    {
        for(int i = 0; i <weaponData[index].count; i++)
        {
            Transform bullet;

            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
               bullet = GameManager.instance.pool.Get(weaponData[index].prefapId).transform;
               bullet.parent = transform;
            }
            
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / weaponData[index].count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(weaponData[index].damage, -1, Vector3.zero); // -1 : 무한 관통
        }
    }

    void Fire()
    {
        if(!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(weaponData[index].prefapId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(weaponData[index].damage, weaponData[index].count, dir);
    }
}
[System.Serializable]
public class WeaponData
{
    public int id;
    public int prefapId;
    public float damage;
    public float speed;
    public int count;
}
