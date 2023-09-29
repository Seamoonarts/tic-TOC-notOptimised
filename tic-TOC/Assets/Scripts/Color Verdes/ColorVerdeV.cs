using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorVerdeV : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    public SpriteRenderer spriteRenderer;
    public Sprite GrillaVerde;
    public Sprite PisaFono;

    public Serial scriptSerial;

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
        if (scriptSerial.baldosaV == true)
        {
            estadoBaldosa = EstadoActivo;
        }
    }

    void EstadoInactivo()
    {
        spriteRenderer.sprite = GrillaVerde;
            
    }

    void EstadoActivo()
    {
        spriteRenderer.sprite = PisaFono;

       // Debug.Log("FonoVActivo");
        if (Input.GetKey(KeyCode.V))
        {
            estadoBaldosa = EstadoInactivo;
            scriptSerial.baldosaV = false;
        }
    }
}

