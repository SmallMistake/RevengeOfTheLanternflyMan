using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ISwitch : MonoBehaviour
{
    public bool pressed;

    public Sprite onSprite;
    public Sprite offSprite;
    internal SpriteRenderer spriteRenderer;

    public GameObject targetObject;
    internal Triggerable targetObjectTrigger;

    private UnityEvent<bool> SwitchTriggered = new UnityEvent<bool>();

    // Start is called before the first frame update
    void Start()
    {
        if(targetObject != null)
        {
            targetObjectTrigger = targetObject.GetComponent<Triggerable>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
    }

    public UnityEvent<bool> GetTriggerEvent()
    {
        return SwitchTriggered;
    }

    public void SetState(bool newState)
    {
        pressed = newState;
        SetSprite();
    }


    internal void TriggerSwitch()
    {
        pressed = !pressed;
        SwitchTriggered.Invoke(pressed);
    }


    internal void SetSprite()
    {
        if (pressed)
        {
            spriteRenderer.sprite = onSprite;
            if (targetObjectTrigger != null)
            {
                targetObjectTrigger.trigger();
            }
        }
        else
        {
            spriteRenderer.sprite = offSprite;
        }
    }
}
