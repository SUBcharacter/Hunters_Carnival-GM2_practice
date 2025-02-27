using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹들을 보관할 변수(배열)
    public GameObject[] enemies;
    // 풀 담당을 하는 리스트들
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
        // 선택한 풀의 놀고 있는 게임 오브젝트 접근
        

        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                // 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // 놀고있는 놈이 없으면? 새로 생성해서  Select에 할당
        if (!select)
        {
            select = Instantiate(enemies[index], transform);
            pools[index].Add(select);
        }


        return select;
    }
}
