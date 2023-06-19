using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRojaU : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    public SpriteRenderer spriteRenderer;
    public Sprite BaldosaRoja;
    public Sprite GrillaRoja;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            spriteRenderer.sprite = BaldosaRoja;
        }
        else if (Input.GetKeyUp(KeyCode.U))
        {
            spriteRenderer.sprite = GrillaRoja;
        }
    }
}
