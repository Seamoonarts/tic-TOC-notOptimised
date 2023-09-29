using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorVerdeA : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    public SpriteRenderer spriteRenderer;
    public Sprite GrillaVerde;
    public Sprite PisaLuz;

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
        if (scriptSerial.baldosaA == true)
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
        spriteRenderer.sprite = PisaLuz;

        
        if (Input.GetKey(KeyCode.A))
        {
    //        Debug.Log("Luz A false");
            estadoBaldosa = EstadoInactivo;
            scriptSerial.baldosaA = false;
        }
    }
}
