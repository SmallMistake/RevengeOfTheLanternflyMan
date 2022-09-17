using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.name);
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            health -= damageDealer.damage;
        }
    }
}
