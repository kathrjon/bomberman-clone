using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using BombermanTools;

public class ExitBehavior : MonoBehaviour
{

    LevelManager levelSettings;
    public GameObject exitLevelEnemyPrefab;

    public int iLevelToLoad = 1;
    public bool boolDoorIsExposed = false;
    public float spawnRate = 10f;
    public float timeSinceLastSpawn;
    // Start is called before the first frame update
    void Start()
    {
        GameObject levelManager = GameObject.Find("LevelManager");
        levelSettings = levelManager.GetComponent<LevelManager>();
        exitLevelEnemyPrefab = levelSettings.exitEnemySpawnPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if(boolDoorIsExposed == true){
            if (timeSinceLastSpawn > 0)
            {
                timeSinceLastSpawn -= Time.deltaTime;
            } else
            {
                GameObject enemy = Instantiate(exitLevelEnemyPrefab, transform.position, Quaternion.identity);
                CircleCollider2D spaceCheck = enemy.transform.Find("SpaceCheck").gameObject.GetComponent<CircleCollider2D>();
                spaceCheck.enabled = false;
                timeSinceLastSpawn = spawnRate;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collider: " + col.tag);
        if (col.tag == "Player")
        {
            LoadScene();
        }
        if(col.tag == "explosion")
        {
            boolDoorIsExposed = true;
            timeSinceLastSpawn = spawnRate;
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(iLevelToLoad);
    }
}
