// Author - Jacob

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;

public class DestructableWallsRandomizer : MonoBehaviour
{

    // [SerializeField] Tile destructable_wall_tile;
    [SerializeField] GameObject exitTilePrefab;
    [SerializeField] Tilemap destructable_tile_map;
    [SerializeField] Tilemap indestructable_tile_map;
    [SerializeField] Tilemap interactable_tile_map;
    [SerializeField] Tilemap bg;
    [SerializeField] GameObject player;

    //Powerup Settings

    [SerializeField] GameObject fireUpPrefab;
    [SerializeField] GameObject speedUpPrefab;
    [SerializeField] GameObject bombUpPrefab;
    [SerializeField] int maxNumberOfPowerUps;

    private int currentEnemyCount;
    private List<GameObject> enemies = new List<GameObject>();
//    private GameObject[] enemies;

    public int number_of_destructable_walls = 20;
    public float chance_of_spawning_wall = 0.25f;
    public Vector3Int bm_starting_position;

    private List<Vector3Int> destructable_block_positions = new List<Vector3Int>();
    private List<EnemySettings> enemiesToInstantiate = new List<EnemySettings>();
    private List<GameObject> powerUps;

    [SerializeField] public GameObject wall_prefab;

    LevelManager levelSettings;

    void Start()
    {
        player = GameObject.Find("Player(Clone)");
        powerUps = new List<GameObject> { fireUpPrefab, speedUpPrefab, bombUpPrefab };
        bg = GameObject.Find("TileMap_Background").gameObject.GetComponent<Tilemap>();

        // if(!this.wall_prefab) {
        //     Debug.Log("Try resource load");
        //     this.wall_prefab = (GameObject)Resources.Load("Lvl_1_breakable_wall");
        //     if( this.wall_prefab == null) {
        //         Debug.Log("Try gameobject load");
        //         this.wall_prefab = GameObject.Find("Lvl_1_breakable_wall");
        //         if(this.wall_prefab == null) {
        //             Debug.Log("Try tag load");
        //             this.wall_prefab = GameObject.FindGameObjectWithTag("Lvl_1_breakable_wall");
        //         }
        //     }
        // }





        //get script settings from level manager
        GameObject levelManager = GameObject.Find("LevelManager");
        levelSettings = levelManager.GetComponent<LevelManager>();
        enemiesToInstantiate = levelSettings.allEnemySettings;

        this.FindBombermanSpawnPoint();
        this.PlaceRandomDestructableBlocks();
        for (var k = 0; k< enemiesToInstantiate.Count; k++)
        {
            currentEnemyCount = 0;
            this.PlaceEnemies(enemiesToInstantiate[k]);
        }
        this.CenterEnemies();
        this.DisableSpaceCheckColliders();
        this.PlaceExit();
        this.PlacePowerUps();
    }

    private void PlaceRandomDestructableBlocks()
    {
        // Loop through indestructable walls so we know where NOT to place destructable wall
        foreach (Vector3Int pos in this.indestructable_tile_map.cellBounds.allPositionsWithin)
        {
            if (!this.indestructable_tile_map.HasTile(pos))
            {
                // No tile here, place destructable wall
                this.PlaceDestructableWall(pos);
            }
        }
    }

    private void PlaceDestructableWall(Vector3Int wall_position)
    {
        // Do nothing if tile spawn chance is greater than class property chance_of_spawning_wall,
        // if number_of_destructable walls has been reached, or wall_position is the same as bombermans current tile.
        if (Random.Range(0f, 1f) > this.chance_of_spawning_wall) return;
        if (this.number_of_destructable_walls-- <= 0) return;
        if (wall_position.x == this.bm_starting_position.x && wall_position.y == this.bm_starting_position.y) return;
        foreach (GameObject enemy in enemies) {
            if (wall_position.x == enemy.transform.position.x && wall_position.y == enemy.transform.position.y) return;
        }

        this.destructable_block_positions.Add(wall_position);

        Vector3 centered_position = BMTiles.GetCellCenter(wall_position, this.destructable_tile_map);
        centered_position.z = 1;
        try {
            Instantiate(this.wall_prefab, centered_position, Quaternion.identity);
        } catch (System.Exception ex) {
            Debug.Log("Couldn't place wall\r\n" + ex);
        }

        // BMTiles.SetTile(wall_position, this.destructable_tile_map, this.destructable_wall_tile);
    }

    private void FindBombermanSpawnPoint()
    {
        this.bm_starting_position = this.destructable_tile_map.WorldToCell(this.player.transform.position);
    }

    private void PlaceExit()
    {
        // Exit exists behind breakable wall

        // Find random wall to place exit behind
        Vector3 exit_position = this.destructable_block_positions.GetRange(Random.Range(0, this.destructable_block_positions.Count - 1), 1)[0];
        Debug.Log("Exit Position " + exit_position);
        Vector3 centeredExit = BMTiles.GetCellCenter(exit_position, interactable_tile_map);
        centeredExit.z = 2;
        Instantiate(exitTilePrefab, centeredExit, Quaternion.identity);
    }

    private void PlacePowerUps()
    {
        for (var i = 0; i < maxNumberOfPowerUps; i++) {
            int index = Random.Range(0, powerUps.Count);
            GameObject powerUp = powerUps[index];
            Vector3 powerup_position = this.destructable_block_positions.GetRange(Random.Range(0, this.destructable_block_positions.Count - 1), 1)[0];
            Vector3 powerup_center_position = BMTiles.GetCellCenter(powerup_position, interactable_tile_map);
            powerup_center_position.z = 2;
            Instantiate(powerUp, powerup_center_position, Quaternion.identity);
        }
    }

    private void PlaceEnemies(EnemySettings enemySetting) {
        foreach (Vector3Int pos in this.indestructable_tile_map.cellBounds.allPositionsWithin)
        {
            if (!this.indestructable_tile_map.HasTile(pos) && !this.destructable_tile_map.HasTile(pos))
            {
                PlaceEnemy(pos, enemySetting);
            }
        }
    }

   private void PlaceEnemy(Vector3Int tile_position, EnemySettings enemySetting){
       if (Random.Range(0f, 1f) > enemySetting.chanceOfSpawningEnemy) return;
       if (tile_position.x == this.bm_starting_position.x && tile_position.y == this.bm_starting_position.y) return;
       foreach (GameObject enemy in enemies)
       {
            CircleCollider2D spaceCheck = enemy.transform.Find("SpaceCheck").gameObject.GetComponent<CircleCollider2D>();
            if (spaceCheck.bounds.Contains(tile_position)) return;
       }
       if (currentEnemyCount < enemySetting.maxEnemyCount){
          GameObject enemy = Instantiate(enemySetting.enemyPrefab, tile_position, Quaternion.identity);
          enemies.Add(enemy);
          currentEnemyCount++;
       }
   }

    private void CenterEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Vector3 cellCenter = BMTiles.GetCellCenter(enemy.transform.position, bg);
            enemy.transform.position = cellCenter;
        }
    }

    private void DisableSpaceCheckColliders()
    {
        foreach (GameObject enemy in enemies)
        {
            CircleCollider2D spaceCheck = enemy.transform.Find("SpaceCheck").gameObject.GetComponent<CircleCollider2D>();
            spaceCheck.enabled = false;
        }
    }
}
