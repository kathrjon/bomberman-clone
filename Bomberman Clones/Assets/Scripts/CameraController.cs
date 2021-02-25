using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{

    [SerializeField] GameObject player;
    public Vector3Int offset = new Vector3Int(0, 0, -11);
    public float smoothSpeed = 0.005f;

    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float bottomLimit;
    [SerializeField] float topLimit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Debug.Log(offset.z);
    }

    void FixedUpdate()
    {
        Vector3Int desiredPosition = new Vector3Int((int)player.transform.position.x + offset.x, (int)player.transform.position.y + offset.y, -11);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        this.transform.position = smoothedPosition;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
            transform.position.z
        );
    }
}
