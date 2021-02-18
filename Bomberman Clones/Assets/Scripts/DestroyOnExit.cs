using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : StateMachineBehaviour
{
    Rigidbody2D bombRb;
    [SerializeField] public Rigidbody2D explosion;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, stateInfo.length);
        Rigidbody2D clone;
       clone = Instantiate(explosion, animator.gameObject.transform);
    }
}
