using UnityEngine;
using static Utils;

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

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(rb.velocity.x, rb.velocity.y, 0).normalized, 0.6f, LayerMask.GetMask("ProjectileInterceptor"));
        Debug.DrawRay(transform.position, new Vector3(rb.velocity.x, rb.velocity.y, 0).normalized, Color.green);
        if (hit.collider != null)
        {
            rb.velocity = Vector2.Reflect(rb.velocity, hit.normal);
            float rotationAdditive;
            if(rb.velocity.x < 0)
            {
                rotationAdditive = -90;
            }
            else
            {
                rotationAdditive = 90;
            }
            float rotation = Mathf.Atan(rb.velocity.y / rb.velocity.x) * Mathf.Rad2Deg - rotationAdditive;
            rb.rotation = rotation;
        }
    }

    public void setShooter(GameObject shooter)
    {
        this.shooter = shooter;
    }

    public GameObject getShooter()
    {
        return shooter;
    }

    public bool getTriggerInteractables()
    {
        return triggerInteractbles;
    }

    internal bool ShotBy(string checkName)
    {
        if (shooter != null && shooter.tag == checkName)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reflect()
    {
        gameObject.layer = 0;
        transform.Rotate(new Vector3(0, 0, 180));
        rb.velocity = -rb.velocity;
    }
}
