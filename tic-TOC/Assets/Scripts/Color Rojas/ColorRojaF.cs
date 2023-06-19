using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRojaF : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            spriteRenderer.sprite = BaldosaRoja;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            spriteRenderer.sprite = GrillaRoja;
        }
    }
}
