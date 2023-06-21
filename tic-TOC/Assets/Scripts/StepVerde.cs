using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepVerde : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite BaldosaStep;
    public Sprite GrillaVerde;
    public Animator animator;
    int traduceBaldosas;

    public Serial scriptSerial;

    void Start()
    {
        spriteRenderer.sprite = GrillaVerde;
        traduceBaldosas = scriptSerial.cuantoBaldosa;
    }

    // scriptSerial.contadorPisadas;
    // spriteRenderer.sprite = BaldosaStep;

    void Update()
    {
        traduceBaldosas = scriptSerial.cuantoBaldosa;
        if (scriptSerial.baldosaActivo == true)
        {
            animator.SetInteger("baldosasPisadas", traduceBaldosas);        
        } 

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (scriptSerial.baldosaActivo == true)
            {
                scriptSerial.cuantoBaldosa = scriptSerial.cuantoBaldosa - 1;
            }
            else if (Input.GetKeyUp(KeyCode.M))
            {
                spriteRenderer.sprite = GrillaVerde;
            }
            if (scriptSerial.cuantoBaldosa == 0)
            {
                scriptSerial.tutorial = false;
                Debug.Log("FIN TUTORIAL");
                Debug.Log(scriptSerial.tutorial);
            }
        }

        
    }

}
