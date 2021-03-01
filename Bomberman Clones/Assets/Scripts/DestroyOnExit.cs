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
    public float explosionPower = 2f;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        findExplosionPath(animator.gameObject.transform);
        Vector3 flameCenter = new Vector3(animator.gameObject.transform.position.x, animator.gameObject.transform.position.y, animator.gameObject.transform.position.z);
        Destroy(animator.gameObject, stateInfo.length);
        Instantiate(explosionCenter, flameCenter, Quaternion.identity);
        for (var i = 0; i < flameList.Count; i++){
            if (flameList[i].isEnd)
            {
                Rigidbody2D flame = Instantiate(explosionVerticalEndDown, flameList[i].flameLocation, Quaternion.identity);
                if (flameList[i].direction == Vector2.left){
                    flame.transform.Rotate(Vector3.forward * -90);
                } else if(flameList[i].direction == Vector2.up){
                    flame.transform.Rotate(Vector3.forward * -180);
                } else if(flameList[i].direction == Vector2.right){
                    flame.transform.Rotate(Vector3.forward * -270);
                }

            } else
            {
                if(flameList[i].direction != Vector2.up && flameList[i].direction != Vector2.down)
                {
                    Rigidbody2D flame = Instantiate(explosionVerticalMiddle, flameList[i].flameLocation, Quaternion.identity);
                    flame.transform.Rotate(Vector3.forward * -90);

                } else
                {
                    Instantiate(explosionVerticalMiddle, flameList[i].flameLocation, Quaternion.identity);
                }
            }
        }
    }

    void findExplosionPath(Transform transform){
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, explosionPower, barrierLayer);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, explosionPower, barrierLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, explosionPower, barrierLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, explosionPower, barrierLayer);

        getExplosionSpriteCellCenters(hitDown, Vector2.down, transform);
        getExplosionSpriteCellCenters(hitUp, Vector2.up, transform);
        getExplosionSpriteCellCenters(hitRight, Vector2.right, transform);
        getExplosionSpriteCellCenters(hitLeft, Vector2.left, transform);

    }

    void getExplosionSpriteCellCenters(RaycastHit2D hit, Vector2 direction, Transform transform){
        if (hit.collider != null){
            float hitDistanceToBarrier = hit.distance;
            for (int i = 1; i <= explosionPower; i++){
                Vector2 directionMultiplied = direction * i;
                Vector2 positionToPlaceExplosion = new Vector2(transform.position.x + directionMultiplied.x, transform.position.y + directionMultiplied.y);
                float distanceToExplosionCenter = Vector3.Distance(transform.position, positionToPlaceExplosion);
                if (distanceToExplosionCenter < hitDistanceToBarrier){
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
