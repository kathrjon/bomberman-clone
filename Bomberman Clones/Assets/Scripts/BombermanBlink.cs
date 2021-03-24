using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanBlink : MonoBehaviour
{

    [SerializeField] private Material matDefault;
    [SerializeField] private Material matWhite;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float blinkTime = 60;
    bool startCoroutine = true;


    // Update is called once per frame
    void Update()
    {
        if (startCoroutine == true)
        {
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        startCoroutine = false;
        while (blinkTime>0)
        {
            if (blinkTime%2 == 0)
            {
                spriteRenderer.material = matWhite;
                blinkTime--;
                yield return new WaitForSeconds(.5f);
            } else
            {
                Debug.Log("Else Triggered");
                spriteRenderer.material = matDefault;
                blinkTime--;
                yield return new WaitForSeconds(.5f);
            }
        }
    }
}
