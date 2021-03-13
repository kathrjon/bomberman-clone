using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartSettings : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemyCount;
    public float chanceOfSpawningEnemy;

    public EnemySettings buildEnemySettings(GameObject enemyPrefab, int maxEnemyCount, float chanceOfSpawningEnemy)
    {
        return new EnemySettings(enemyPrefab, maxEnemyCount, chanceOfSpawningEnemy);
    }
}

public class EnemySettings {
    public GameObject enemyPrefab;
    public int maxEnemyCount;
    public float chanceOfSpawningEnemy;
    public EnemySettings(GameObject enemyPrefab, int maxEnemyCount, float chanceOfSpawningEnemy)
    {
        this.enemyPrefab = enemyPrefab;
        this.maxEnemyCount = maxEnemyCount;
        this.chanceOfSpawningEnemy = chanceOfSpawningEnemy;
    }

    public GameObject EnemyPrefab
    {
        get { return enemyPrefab; }
        set { enemyPrefab = value; }
    }

    public int MaxEnemyCount
    {
        get { return maxEnemyCount; }
        set { maxEnemyCount = value; }
    }

    public float ChanceOfSpawningEnemy
    {
        get { return chanceOfSpawningEnemy; }
        set { chanceOfSpawningEnemy = value; }
    }
}