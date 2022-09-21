using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : ISwitch
{
    public Sprite onSprite;
    public Sprite offSprite;
    private SpriteRenderer spriteRenderer;
    private bool on = false;

    public GameObject targetObject;
    Triggerable targetObjectTrigger;
    // Start is called before the first frame update
    void Start()
    {
        targetObjectTrigger = targetObject.GetComponent<Triggerable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<FallScript>())
        {
            on = !on;
            SetSprite();
        }
    }

    private void SetSprite()
    {
        if (on)
        {
            spriteRenderer.sprite = onSprite;
            targetObjectTrigger.trigger();
        }
        else
        {
            spriteRenderer.sprite = offSprite;
        }
    }
}
