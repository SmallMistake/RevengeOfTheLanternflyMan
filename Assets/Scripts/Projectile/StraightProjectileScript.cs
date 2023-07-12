using UnityEngine;

public class StraightProjectileScript : IProjectile
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
