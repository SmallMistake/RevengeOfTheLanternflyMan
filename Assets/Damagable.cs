using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageDealer damageDealer = collision.otherCollider.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            health -= damageDealer.damage;
            if(health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(transform.parent.gameObject);
    }
}
