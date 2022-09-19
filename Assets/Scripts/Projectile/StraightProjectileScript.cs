using UnityEngine;

public class StraightProjectileScript : IProjectile
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Bouncer"))
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomBounds"))
        {
            Destroy(gameObject);
        }
    }
}
