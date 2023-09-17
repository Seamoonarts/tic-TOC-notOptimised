using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRojaN : MonoBehaviour
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
        if (Input.GetKey(KeyCode.N))
        {
            spriteRenderer.sprite = BaldosaRoja;
            while (i == 1)
            {
                scriptSerial.CaosActivoParaBaldosa();
                i = 2;
            }
        }
        else if (Input.GetKeyUp(KeyCode.N))
        {
            spriteRenderer.sprite = GrillaRoja;
            scriptSerial.CaosNoActivo();
            i = 1;
        }
    }
}
