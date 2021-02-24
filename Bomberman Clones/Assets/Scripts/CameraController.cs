using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{

    [SerializeField] GameObject player;
    public Vector3Int offset = new Vector3Int(0, 0, -11);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3Int((int)player.transform.position.x + offset.x, (int)player.transform.position.y + offset.y, -11);
        // Debug.Log(offset.z);
    }
}
