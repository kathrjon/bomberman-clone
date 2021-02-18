using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructableTiles : MonoBehaviour{
    public Tilemap destructableTilemap;

    private void Start(){
        destructableTilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.CompareTag("explosion"))
        {
            Debug.Log(collider.gameObject.tag);
            Vector3 hitPosition = collider.gameObject.transform.position;
            destructableTilemap.SetTile(destructableTilemap.WorldToCell(hitPosition), null);
        }
    }
}
