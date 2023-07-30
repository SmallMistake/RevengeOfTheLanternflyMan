using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite offSprite;
    public Sprite onSprite;
    private bool on;
    public GameObject triggerTarget;
    private Triggerable trigger;

    private void Start()
    {
        trigger = triggerTarget.GetComponent<Triggerable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IProjectile projectile = collision.gameObject.GetComponent<IProjectile>();
        if (projectile && projectile.triggerInteractbles)
        {
            on = !on;
            if (on)
            {
                spriteRenderer.sprite = onSprite;
                trigger.trigger();
            }
            else
            {
                spriteRenderer.sprite = offSprite;
            }
        }
    }
}
