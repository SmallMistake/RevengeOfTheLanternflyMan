using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchController : MonoBehaviour
{
    public TagMask tagMask = new TagMask();

    public SpriteRenderer spriteRenderer;
    public Sprite offSprite;
    public Sprite onSprite;
    private bool on;

    // switch delegate
    public delegate void OnSwitchPressed(bool value);
    public OnSwitchPressed onSwitchPressed;

    public UnityEvent onSwitchOn;
    public UnityEvent onSwitchOff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( tagMask.IsInTagMask(collision.tag)) { 
            on = !on;
            onSwitchPressed?.Invoke(on);
            if (on)
            {
                spriteRenderer.sprite = onSprite;
                onSwitchOn?.Invoke();
            }
            else
            {
                spriteRenderer.sprite = offSprite;
                onSwitchOff?.Invoke();
            }
        }
    }

    public bool isOn()
    {
        return on;
    }
}
