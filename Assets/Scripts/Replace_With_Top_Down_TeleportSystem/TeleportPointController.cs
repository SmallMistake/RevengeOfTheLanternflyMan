using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class TeleportPointController : MonoBehaviour
{
    public TagMask mask = new TagMask();
    public TeleportPointController destination;
    private bool onCooldown;

    public UnityEvent preTeleportEvents;
    public UnityEvent postTeleportEvents;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Teleportable teleportable = collision.GetComponent<Teleportable>();
        if (mask.IsInTagMask(collision.tag) && teleportable && destination){
            if (!onCooldown) {
                preTeleportEvents?.Invoke();
                destination.TeleportToThisPoint(teleportable.getObjectToTeleport());

            }
        }
    }

    private void TeleportToThisPoint(GameObject movingObject)
    {
        movingObject.transform.position = gameObject.transform.position;
        postTeleportEvents?.Invoke();
        StartCoroutine(HandleCooldownTimer());
    }

    IEnumerator HandleCooldownTimer()
    {
        onCooldown = true;
        yield return new WaitForSeconds(0.05f);
        onCooldown = false;
    }
}
