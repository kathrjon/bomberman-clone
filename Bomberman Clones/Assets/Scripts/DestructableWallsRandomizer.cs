// Author - Jacob

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;

public class DestructableWallsRandomizer : MonoBehaviour
{

    [SerializeField] Tile tile;
    [SerializeField] Tilemap destructable_tile_map;
    [SerializeField] Tilemap indestructable_tile_map;

    public int number_of_destructable_walls = 40;
    public float chance_of_spawning_wall = 0.25f;

    void Start()
    {
        this.PlaceRandomDestructableBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaceRandomDestructableBlocks() 
    {
        // Loop through destructable walls so we know where NOT to place destructable wall
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
        // Do nothing if tile spawn chance is greater than class property chance_of_spawning_wall
        // or if number_of_destructable walls has been reached. 
        if(Random.Range(0f, 1f) > this.chance_of_spawning_wall) return;
        if(this.number_of_destructable_walls-- <= 0) return;

        BMTiles.SetTile(tile_position, this.destructable_tile_map, this.tile);        
    }

}
