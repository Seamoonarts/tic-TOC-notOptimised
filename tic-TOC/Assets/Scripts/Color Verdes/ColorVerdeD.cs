using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorVerdeD : MonoBehaviour
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
        if (scriptSerial.baldosaD == true)
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
        spriteRenderer.sprite = GrillaVerde;

    }

    void EstadoActivo()
    {
        spriteRenderer.sprite = PisaLuz;

   //     Debug.Log("Luz D Activo");
        if (Input.GetKey(KeyCode.D))
        {
            estadoBaldosa = EstadoInactivo;
            scriptSerial.baldosaD = false;
        }
    }
}