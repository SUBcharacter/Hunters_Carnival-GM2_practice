using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public Player player;
    public PoolManager pool;
    [Header("# Player Info")]
    public int level=0;
    public int kill;
    public int exp;
    public int[] nextExp;
    [Header("# Game Object")]
    public float gameTime;
    public static float maxGameTime = 10 * 10f;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
