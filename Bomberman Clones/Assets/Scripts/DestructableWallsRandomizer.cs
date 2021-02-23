// Author - Jacob

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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


    private void PlaceRandomDestructableBlocks() {
        // var bounds = this.destructable_tile_map.cellBounds;
        // TileBase[] destructable_map_tiles = this.destructable_tile_map.GetTilesBlock(this.destructable_tile_map.cellBounds);
        // for(int x = 0; x < bounds.x; x++) {
        //     for(int y = 0; y < bounds.y; y++) {
        //         if(this.destructable_tile_map.HasTile(new Vector3Int()))
        //     }
        // }

        // Loop through destructable walls so we know where NOT to place destructable wall
        foreach(var pos in this.indestructable_tile_map.cellBounds.allPositionsWithin) {
            Vector3Int tile = new Vector3Int(pos.x, pos.y, pos.z);
            if(!this.indestructable_tile_map.HasTile(tile)) {
                // No tiles here, place destructable wall
                this.PlaceDestructableWall(pos.x, pos.y, pos.z);
            } else {
                // Debug.Log("Unbreakable tile exists, skipping");
            }
            // Debug.Log("=====================================");
        }

    }

    private void PlaceDestructableWall(int x, int y, int z) {

        // var fl = Random.Range(0f, 1f);
        // Debug.Log(fl);
        // Debug.Assert((fl > this.chance_of_spawning_wall));

        if(Random.Range(0f, 1f) > this.chance_of_spawning_wall) return;
        if(this.number_of_destructable_walls-- <= 0) return;
        this.destructable_tile_map.SetTile(new Vector3Int(x, y, z), this.tile);
        Debug.Log("Placing tile at " + x + ", " + y);
    }

}
