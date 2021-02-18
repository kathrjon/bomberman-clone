using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombbehaviour : MonoBehaviour
{
    Animator anim;
    [SerializeField] public Rigidbody2D explosion;

    void Start()
    {
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
//        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Bomb Animation")){
//            Destroy(anim.gameObject);
//            Rigidbody2D clone;
//            clone = Instantiate(explosion, anim.gameObject.transform);
//        }
    }
}
