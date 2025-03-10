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
    public int health;
    public int maxHealth = 100;
    public int level=1;
    public int maxLevel;
    public int kill;
    public int exp;
    public int[] nextExp;
    [Header("# Game Object")]
    public float gameTime;
    public float maxGameTime = 10 * 10f;

    void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        health = maxHealth;
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
            if (level == maxLevel)
            {
                exp = nextExp[maxLevel - 1];
            }
            else
                exp = 0;
        }
    }
}
