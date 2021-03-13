using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] EnemyStartSettings enemySettings;

    //Exit Spawn Enemy
    [SerializeField] public GameObject exitEnemySpawnPrefab;

    //Puropen Settings
    [SerializeField] private GameObject puropenPrefab;
    [SerializeField] private int maxNumberOfPuropens;
    [SerializeField] private float chanceOfPuropenSpawning;

    //Starnuts Settings
    [SerializeField] private GameObject starnutsPrefab;
    [SerializeField] private int maxNumberOfStarNuts;
    [SerializeField] private float chanceOfStarNutsSpawning;

    //Denktun Settings
    [SerializeField] private GameObject denktunPrefab;
    [SerializeField] private int maxNumberOfDenktun;
    [SerializeField] private float chanceOfDenktunSpawning;

    public List<EnemySettings> allEnemySettings = new List<EnemySettings>();

    void Awake()
    {
        checkToAddEnemySettings(puropenPrefab, maxNumberOfPuropens, chanceOfPuropenSpawning);
        checkToAddEnemySettings(starnutsPrefab, maxNumberOfStarNuts, chanceOfStarNutsSpawning);
        checkToAddEnemySettings(denktunPrefab, maxNumberOfDenktun, chanceOfDenktunSpawning);
    }

    void checkToAddEnemySettings(GameObject enemyPrefab, int maxNumberOfEnemy, float chanceOfEnemySpawning)
    {
        if (maxNumberOfEnemy > 0){
            addEnemySetting(enemyPrefab, maxNumberOfEnemy, chanceOfEnemySpawning);
        }
    }

    void addEnemySetting(GameObject enemyPrefab, int maxNumberOfEnemy, float chanceOfEnemySpawning)
    {
        EnemySettings individualEnemySettings = enemySettings.buildEnemySettings(enemyPrefab, maxNumberOfEnemy, chanceOfEnemySpawning);
        allEnemySettings.Add(individualEnemySettings);
    }
}
