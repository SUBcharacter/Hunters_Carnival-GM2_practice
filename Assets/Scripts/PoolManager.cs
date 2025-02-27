using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �����յ��� ������ ����(�迭)
    public GameObject[] enemies;
    // Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[enemies.Length];

        for(int i=0;i<pools.Length;i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        // ������ Ǯ�� ��� �ִ� ���� ������Ʈ ����
        

        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                // �߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ����ִ� ���� ������? ���� �����ؼ�  Select�� �Ҵ�
        if (!select)
        {
            select = Instantiate(enemies[index], transform);
            pools[index].Add(select);
        }


        return select;
    }
}
