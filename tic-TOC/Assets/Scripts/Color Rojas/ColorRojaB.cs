using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRojaB : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    public SpriteRenderer spriteRenderer;
    public Sprite BaldosaRoja;
    public Sprite GrillaRoja;

    public Serial scriptSerial;


    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.B))
            {
                spriteRenderer.sprite = BaldosaRoja;
                scriptSerial.CaosActivo();
        }
            else if (Input.GetKeyUp(KeyCode.B))
            {
                spriteRenderer.sprite = GrillaRoja;
            }
    }
}
