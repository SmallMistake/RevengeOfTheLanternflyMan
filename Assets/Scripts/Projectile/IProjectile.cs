using UnityEngine;

public class IProjectile : MonoBehaviour
{
    public float speed;
    internal GameObject shooter;
    public bool triggerInteractbles = false;  //used to say if a projectile flips switches ect
    public GameObject hitEffect;
    internal Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 movement = Vector2.up * speed;
        rb.velocity = rb.GetRelativeVector(movement);
    }

    public bool getTriggerInteractables()
    {
        return triggerInteractbles;
    }

    private void OnDestroy()
    {
        if (hitEffect)
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
        }
    }

}
