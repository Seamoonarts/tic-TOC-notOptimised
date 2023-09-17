using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRojaH : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    public SpriteRenderer spriteRenderer;
    public Sprite BaldosaRoja;
    public Sprite GrillaRoja;

    public Serial scriptSerial;
    int i = 1;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            spriteRenderer.sprite = BaldosaRoja;
            while (i == 1)
            {
                scriptSerial.CaosActivoParaBaldosa();
                i = 2;
            }
        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
            spriteRenderer.sprite = GrillaRoja;
            scriptSerial.CaosNoActivo();
            i = 1;
        }
    }
}
