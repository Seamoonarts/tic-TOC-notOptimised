using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorVerdeG : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    public SpriteRenderer spriteRenderer;
    public Sprite BaldosaRoja;
    public Sprite GrillaRoja;
    public Sprite GrillaVerde;

    public Serial scriptSerial;

    private bool bAct = false;
    int i = 1;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        bAct = scriptSerial.baldosaActivo;
        if (bAct)
        {
            spriteRenderer.sprite = GrillaRoja;
        }else
        {
            spriteRenderer.sprite = GrillaVerde;
        }

        if (Input.GetKey(KeyCode.G) && bAct == true)
        {
            spriteRenderer.sprite = BaldosaRoja;
            while(i == 1)
            {
                scriptSerial.CaosActivoParaBaldosa();
                i = 2;
            }
            
        }
        else if(Input.GetKeyUp(KeyCode.G) && bAct == true)
        {
            spriteRenderer.sprite = GrillaRoja;
            i = 1;
        }
        
    }
}
