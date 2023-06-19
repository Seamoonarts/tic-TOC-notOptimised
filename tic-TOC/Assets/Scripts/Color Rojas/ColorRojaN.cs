using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRojaN : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.N))
        {
            spriteRenderer.sprite = BaldosaRoja;
        }
        else if (Input.GetKeyUp(KeyCode.N))
        {
            spriteRenderer.sprite = GrillaRoja;
        }
    }
}
