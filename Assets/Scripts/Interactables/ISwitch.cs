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

    public List<GameObject> targetObjects;
    private List<Triggerable> triggersToCall = new List<Triggerable>();

    private UnityEvent<bool> SwitchTriggered = new UnityEvent<bool>();

    // Start is called before the first frame update
    void Start()
    {
        if(targetObjects != null)
        {
            foreach (GameObject item in targetObjects)
            {
                triggersToCall.Add(item.GetComponent<Triggerable>());
            }
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

    internal void TriggerSwitch(bool state)
    {
        pressed = state;
        SwitchTriggered.Invoke(pressed);
    }


    internal void SetSprite()
    {
        if (pressed)
        {
            spriteRenderer.sprite = onSprite;
            if (targetObjects != null)
            {
                foreach (Triggerable triggerable in triggersToCall)
                {
                    triggerable.trigger();
                }
            }
        }
        else
        {
            spriteRenderer.sprite = offSprite;
        }
    }
}
