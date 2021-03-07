using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGrid : MonoBehaviour
{

    [SerializeField] public Grid gridPrefab;
    private Grid grid;

    // Start is called before the first frame update
    void Awake()
    {
        grid = Instantiate(gridPrefab);
    }
}
