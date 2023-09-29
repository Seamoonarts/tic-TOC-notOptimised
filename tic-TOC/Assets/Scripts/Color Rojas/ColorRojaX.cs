using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRojaX : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    public SpriteRenderer spriteRenderer;
    public Sprite BaldosaRoja;
    public Sprite GrillaRoja;
    public Sprite PisaFono;

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
        if (scriptSerial.baldosaX == true)
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
        if (Input.GetKey(KeyCode.X))
        {
            spriteRenderer.sprite = BaldosaRoja;
            while (i == 1)
            {
                scriptSerial.CaosActivoParaBaldosa();
                i = 2;
            }
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            spriteRenderer.sprite = GrillaRoja;
            scriptSerial.CaosNoActivo();
            i = 1;
        }
    }

    void EstadoActivo()
    {
        spriteRenderer.sprite = PisaFono;

   //     Debug.Log("FonoXActivo");
        if (Input.GetKey(KeyCode.X))
        {
            estadoBaldosa = EstadoInactivo;
            scriptSerial.baldosaX = false;
        }
    }
}

