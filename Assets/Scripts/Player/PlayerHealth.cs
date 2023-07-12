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

    private bool invincible = false; //used for IFrames


    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        health = startingHealth;
        playerHealthChanged.Invoke(health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleTriggerEvent(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HandleTriggerEvent(collision);
    }

    public void HandleTriggerEvent(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer && !invincible)
        {
            invincible = true;
            animator.SetTrigger("StartIFrames");
            DealDamage(damageDealer.damage);
            playerRigidbody.AddForce(new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y).normalized * knockbackForce);
        }
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        playerHealthChanged.Invoke(health);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Hurt");
        if (health <= 0)
        {
            PlayerStateMachine playerStateMachine = GetComponent<PlayerStateMachine>();
            playerStateMachine.Die();
        }
    }

    public void EndIFrames()
    {
        invincible = false;
    }
}
