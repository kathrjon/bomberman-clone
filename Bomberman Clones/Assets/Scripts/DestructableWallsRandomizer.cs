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

    public int number_of_destructable_walls = 20;
    public float chance_of_spawning_wall = 0.25f;
    public Vector3Int bm_starting_position;

    private List<Vector3Int> destructable_block_positions = new List<Vector3Int>();

    void Start()
    {   
        this.FindBombermanSpawnPoint();
        this.PlaceRandomDestructableBlocks();
        this.PlaceExit();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        BMTiles.SetTile(exit_position, this.interactable_tile_map, this.exit_tile);
    }

}
