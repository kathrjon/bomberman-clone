using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LoadGrid : MonoBehaviour
{

    [SerializeField] public Grid gridPrefab;
    [SerializeField] public GameObject playerPrefab;
    private Tilemap[] tilemaps;
    private Grid grid;
    private GameObject player;


    void Awake()
    {
        grid = Instantiate(gridPrefab);
        Vector3 playerStartPostion = new Vector3(-8.5f, 6.5f, 2);
        player = Instantiate(playerPrefab, playerStartPostion, Quaternion.identity);
    }
}
