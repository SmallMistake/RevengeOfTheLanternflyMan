using GBJam.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int startingHealth = 8;
    private int health;
    public static event Action<int> playerHealthChanged;

    private void Start()
    {
        health = startingHealth;
        playerHealthChanged.Invoke(health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            health -= damageDealer.damage;
            playerHealthChanged.Invoke(health);
            if (health <= 0)
            {
                PlayerStateMachine playerStateMachine = transform.parent.GetComponent<PlayerStateMachine>();
                playerStateMachine.Die();
            }
        }
    }
}
