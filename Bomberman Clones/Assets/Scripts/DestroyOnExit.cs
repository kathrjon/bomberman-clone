using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using BombermanTools;

public class DestroyOnExit : StateMachineBehaviour
{
    [SerializeField] public Rigidbody2D explosionCenter;
    [SerializeField] public Rigidbody2D explosionVerticalMiddle;
    [SerializeField] public Rigidbody2D explosionVerticalEndDown;
    [SerializeField] LayerMask barrierLayer;
    [SerializeField] public Tilemap bg;
    [SerializeField] List<Vector3> placeExplosionsDownArray = new List<Vector3>();
    public float explosionPower = 2;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        findExplosionPath(animator.gameObject.transform);
        Debug.Log("placeExplosionsDownArray.Count " + placeExplosionsDownArray.Count);


        Destroy(animator.gameObject, stateInfo.length);
 //       Rigidbody2D clone;
        Instantiate(explosionCenter, animator.gameObject.transform);
        for (var i = 0; i < placeExplosionsDownArray.Count; i++)
        {
            if(i == placeExplosionsDownArray.Count -1)
            {
                Instantiate(explosionVerticalEndDown, placeExplosionsDownArray[i], Quaternion.identity);
            } else
            {
                Instantiate(explosionVerticalMiddle, placeExplosionsDownArray[i], Quaternion.identity);
            }
        }
    }

    void findExplosionPath(Transform transform)
    {
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, explosionPower, barrierLayer);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, explosionPower, barrierLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, explosionPower, barrierLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, explosionPower, barrierLayer);

        if(hitDown.collider != null){
            float hitDistanceToBarrier = hitDown.distance;
            Debug.Log("hitDistanceToBarrier" + hitDistanceToBarrier);
            for(int i = 1; i<explosionPower; i++){
                Vector2 positionToPlaceExplosion = new Vector2(transform.position.x, transform.position.y - i);
                float distanceToExplosionCenter = Vector3.Distance(transform.position, positionToPlaceExplosion);
                Debug.Log("distanceToExplosionCenter " + distanceToExplosionCenter);
                if (distanceToExplosionCenter < hitDistanceToBarrier)
                {
                    placeExplosionsDownArray.Add(positionToPlaceExplosion);
                }
                else
                {
                    Debug.Log("don't place explosion");
                }
            }

        } else if (hitDown.collider == null){
            for (int i = 1; i < explosionPower; i++)
            {
                Vector2 positionToPlaceExplosion = new Vector2(transform.position.x, transform.position.y - i);
                placeExplosionsDownArray.Add(positionToPlaceExplosion);
                Debug.Log("Place Explosion");
            }
        }
    }

}
