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
    public Animator animator;

    float knockbackForce = 7000;

    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        health = startingHealth;
        playerHealthChanged.Invoke(health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            health -= damageDealer.damage;
            playerHealthChanged.Invoke(health);
            if (health <= 0)
            {
                PlayerStateMachine playerStateMachine = GetComponent<PlayerStateMachine>();
                playerStateMachine.Die();
            }
            playerRigidbody.AddForce(new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y).normalized * knockbackForce);
            animator.SetTrigger("StartIFrames");
        }
    }
}
