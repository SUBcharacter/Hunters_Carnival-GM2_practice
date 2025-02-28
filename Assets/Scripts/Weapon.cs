using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    

    public WeaponData[] weaponData;
    void Start()
    {
        Init();
    }
    void Update()
    {
        switch (weaponData[0].id)
        {
            case 0:
                transform.Rotate(Vector3.back * weaponData[0].speed * Time.deltaTime);
                break;
            default: break;
        }
    }

    public void Init()
    {
        switch(weaponData[0].id)
        {
            case 0:
                weaponData[0].speed = 150;
                Inbound();
                break;
            default: break;
        }
    }

    void Inbound()
    {
        for(int i = 0; i <weaponData[0].count; i++)
        {
           Transform bullet =  GameManager.instance.pool.Get(weaponData[0].prefapId).transform;
            bullet.parent = transform;

            Vector3 rotVec = Vector3.forward * 360 * i / weaponData[0].count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(weaponData[0].damage, -1); // -1 : 무한 관통
        }
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
