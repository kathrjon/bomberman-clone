// Author - Jacob

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;

public class DestructableWallsRandomizer : MonoBehaviour
{

    [SerializeField] Tile destructable_wall_tile;
    [SerializeField] Tile exit_tile;
    [SerializeField] Tilemap destructable_tile_map;
    [SerializeField] Tilemap indestructable_tile_map;
    [SerializeField] Tilemap interactable_tile_map;
    [SerializeField] GameObject player;
    [SerializeField] GameObject fireUpPrefab;
    [SerializeField] GameObject speedUpPrefab;
    [SerializeField] GameObject bombUpPrefab;
    [SerializeField] int maxNumberOfPowerUps;
    [SerializeField] public GameObject puropenPrefab;
    [SerializeField] public int puropenCount;
    [SerializeField] public int currentPuropenCount;
    [SerializeField] float chance_of_spawning_puropen = 0.05f;

    private GameObject puropen;
    private GameObject[] enemies;

    public int number_of_destructable_walls = 20;
    public float chance_of_spawning_wall = 0.25f;
    public Vector3Int bm_starting_position;

    private List<Vector3Int> destructable_block_positions = new List<Vector3Int>();
    private List<GameObject> powerUps; 
    //{ fireUpPrefab, speedUpPrefab, bombUpPrefab };

    void Start()
    {
        player = GameObject.Find("Player(Clone)");
        powerUps = new List<GameObject> { fireUpPrefab, speedUpPrefab, bombUpPrefab };
        this.FindBombermanSpawnPoint();
        this.PlaceEnemies();
        this.FindEnemySpawnPoints();
        this.PlaceRandomDestructableBlocks();
        this.PlaceExit();
        this.PlacePowerUps();
    }

    private void PlaceRandomDestructableBlocks() 
    {
        // Loop through indestructable walls so we know where NOT to place destructable wall
        foreach(Vector3Int pos in this.indestructable_tile_map.cellBounds.allPositionsWithin) 
        {
            if(!this.indestructable_tile_map.HasTile(pos)) 
            {
                // No tile here, place destructable wall
                this.PlaceDestructableWall(pos);
            }
        }
    }

    private void PlaceDestructableWall(Vector3Int tile_position) 
    {
        // Do nothing if tile spawn chance is greater than class property chance_of_spawning_wall,
        // if number_of_destructable walls has been reached, or tile_position is the same as bombermans current tile.
        if(Random.Range(0f, 1f) > this.chance_of_spawning_wall) return;
        if(this.number_of_destructable_walls-- <= 0) return;
        if(tile_position.x == this.bm_starting_position.x && tile_position.y == this.bm_starting_position.y) return;
        foreach(GameObject enemy in enemies){
            if (tile_position.x == enemy.transform.position.x && tile_position.y == enemy.transform.position.y) return;
        }

        BMTiles.SetTile(tile_position, this.destructable_tile_map, this.destructable_wall_tile);
        this.destructable_block_positions.Add(tile_position);
    }

    private void FindBombermanSpawnPoint()
    {
        this.bm_starting_position = this.destructable_tile_map.WorldToCell(this.player.transform.position);
    }

    private void PlaceExit()
    {
        // Exit exists behind breakable wall

        // Find random wall to place exit behind
        Vector3Int exit_position = this.destructable_block_positions.GetRange(Random.Range(0, this.destructable_block_positions.Count - 1), 1)[0];
//        exit_position.z = 2;
        Debug.Log("Exit Position " + exit_position);
        BMTiles.SetTile(exit_position, this.interactable_tile_map, this.exit_tile);
    }

    private void PlacePowerUps()
    {
        for (var i = 0; i < maxNumberOfPowerUps; i++){
            int index = Random.Range(0, powerUps.Count);
            GameObject powerUp = powerUps[index];
            Vector3 powerup_position = this.destructable_block_positions.GetRange(Random.Range(0, this.destructable_block_positions.Count - 1), 1)[0];
            Vector3 powerup_center_position = BMTiles.GetCellCenter(powerup_position, interactable_tile_map);
            powerup_center_position.z = 2;
            Instantiate(powerUp, powerup_center_position, Quaternion.identity);
        }
    }

    private void FindEnemySpawnPoints()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void PlaceEnemies(){
        foreach (Vector3Int pos in this.indestructable_tile_map.cellBounds.allPositionsWithin)
        {
            if (!this.indestructable_tile_map.HasTile(pos))
            {
                PlaceEnemy(pos);
            }
        }
    }

    private void PlaceEnemy(Vector3Int tile_position)
    {
        if (Random.Range(0f, 1f) > this.chance_of_spawning_puropen) return;
        if (this.number_of_destructable_walls-- <= 0) return;
        if (tile_position.x == this.bm_starting_position.x && tile_position.y == this.bm_starting_position.y) return;
        if (currentPuropenCount <= puropenCount)
        {
            puropen = Instantiate(puropenPrefab, tile_position, Quaternion.identity);
            currentPuropenCount++;
        }
    }

}
