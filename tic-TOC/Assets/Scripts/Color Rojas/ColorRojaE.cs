using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRojaE : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    public SpriteRenderer spriteRenderer;
    public Sprite BaldosaRoja;
    public Sprite GrillaRoja;
    public Sprite PisaLuz;

    public Serial scriptSerial;
    int i = 1;

    public delegate void EstadoBaldosa();
    public EstadoBaldosa estadoBaldosa;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        estadoBaldosa = EstadoInactivo;

    }

    void Update()
    {
        estadoBaldosa();
        if (scriptSerial.baldosaE == true)
        {
            estadoBaldosa = EstadoActivo;
        }
        else
        {
            estadoBaldosa = EstadoInactivo;
        }
    }

    void EstadoInactivo()
    {
        spriteRenderer.sprite = GrillaRoja;
        if (Input.GetKey(KeyCode.E))
        {
            spriteRenderer.sprite = BaldosaRoja;
            while (i == 1)
            {
                scriptSerial.CaosActivoParaBaldosa();
                i = 2;
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            spriteRenderer.sprite = GrillaRoja;
            scriptSerial.CaosNoActivo();
            i = 1;
        }
    }

    void EstadoActivo()
    {
        spriteRenderer.sprite = PisaLuz;

     //   Debug.Log("FonoUActivo");
        if (Input.GetKey(KeyCode.E))
        {
            estadoBaldosa = EstadoInactivo;
            scriptSerial.baldosaE = false;
        }
    }
}
