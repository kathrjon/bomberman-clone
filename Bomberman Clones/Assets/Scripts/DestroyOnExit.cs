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
    [SerializeField] List<Flame> flameList = new List<Flame>();
    [SerializeField] List<Vector2> directionVectors = new List<Vector2> { Vector2.down, Vector2.up, Vector2.right, Vector2.left };
    public int explosionPower = 1;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        explosionPower = GameObject.Find("Player").GetComponent<playercontroller>().stats.explosionStrength;
        findExplosionPath(animator.gameObject.transform);
        Vector3 flameCenter = new Vector3(animator.gameObject.transform.position.x, animator.gameObject.transform.position.y, animator.gameObject.transform.position.z);
        animator.gameObject.GetComponent<Renderer>().enabled = false;
        Destroy(animator.gameObject, stateInfo.length-2f);
        instantiateExplosion(flameCenter);
    }

    void findExplosionPath(Transform transform){
        //make raycasts in all 4 directions and check for hits
        for (var i = 0; i<directionVectors.Count; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionVectors[i], explosionPower, barrierLayer);
            getExplosionSpriteCellCenters(hit, directionVectors[i], transform);
        }
    }

    void instantiateExplosion(Vector3 flameCenter)
    {
        Instantiate(explosionCenter, flameCenter, Quaternion.identity);
        for (var i = 0; i < flameList.Count; i++)
        {
            if (flameList[i].isEnd)
            {
                Rigidbody2D flame = Instantiate(explosionVerticalEndDown, flameList[i].flameLocation, Quaternion.identity);
                //rotate sprites based on the direction of the flame path
                if (flameList[i].direction == Vector2.left)
                {
                    flame.transform.Rotate(Vector3.forward * -90);
                }
                else if (flameList[i].direction == Vector2.up)
                {
                    flame.transform.Rotate(Vector3.forward * -180);
                }
                else if (flameList[i].direction == Vector2.right)
                {
                    flame.transform.Rotate(Vector3.forward * -270);
                }

            }
            else
            {
                if (flameList[i].direction != Vector2.up && flameList[i].direction != Vector2.down)
                {
                    Rigidbody2D flame = Instantiate(explosionVerticalMiddle, flameList[i].flameLocation, Quaternion.identity);
                    flame.transform.Rotate(Vector3.forward * -90);

                }
                else
                {
                    Instantiate(explosionVerticalMiddle, flameList[i].flameLocation, Quaternion.identity);
                }
            }
        }
    }

    void getExplosionSpriteCellCenters(RaycastHit2D hit, Vector2 direction, Transform transform){
        if (hit.collider != null){
            float hitDistanceToBarrier = hit.distance;
            //Use i to count out the number of tiles to check for potential fire
            for (int i = 1; i <= explosionPower; i++){
                Vector2 directionMultiplied = direction * i;
                Vector2 positionToPlaceExplosion = new Vector2(transform.position.x + directionMultiplied.x, transform.position.y + directionMultiplied.y);
                float distanceToExplosionCenter = Vector3.Distance(transform.position, positionToPlaceExplosion);
                //if the distance from the potential explosion sprite is less that the distance between the raycast origin and the hit point, then add vector to list
                if (distanceToExplosionCenter < hitDistanceToBarrier){
                    //check if the Vector being checked is the end of the flame path
                    if(i == explosionPower)
                    {
                        flameList.Add(new Flame(positionToPlaceExplosion, true, direction));
                    } else
                    {
                        flameList.Add(new Flame(positionToPlaceExplosion, false, direction));
                    }
                }
            }
        } else if (hit.collider == null){
            for (int i = 1; i <= explosionPower; i++){
                Vector2 directionMultiplied = direction * i;
                Vector2 positionToPlaceExplosion = new Vector2(transform.position.x + directionMultiplied.x, transform.position.y + directionMultiplied.y);
                if (i == explosionPower)
                {
                    flameList.Add(new Flame(positionToPlaceExplosion, true, direction));
                }
                else
                {
                    flameList.Add(new Flame(positionToPlaceExplosion, false, direction));
                }
            }
        }
    }
}

public class Flame
{
    public Vector3 flameLocation;
    public bool isEnd;
    public Vector2 direction;

    public Flame(Vector3 flameLocation, bool isEnd, Vector2 direction)
    {
        this.flameLocation = flameLocation;
        this.isEnd = isEnd;
        this.direction = direction;
    }

    public Vector3 FlameLocation
    {
        get { return flameLocation; }
        set { flameLocation = value; }
    }

    public bool IsEnd
    {
        get { return isEnd; }
        set { isEnd = value; }
    }

    public Vector2 Direction
    {
        get { return direction; }
        set { direction = value; }
    }
}
