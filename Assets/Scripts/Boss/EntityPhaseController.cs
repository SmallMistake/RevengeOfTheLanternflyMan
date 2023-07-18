using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityPhaseController : MonoBehaviour
{

    public List<Damagable> damagePoints;
    private int startingHealth;

    private Slider healthBar;


    public void StartPhase()
    {
        startingHealth = GetCurrentHealth();
        SetupDamageListeners();
        UpdateHealthBar();
        print("TODO create timeline to allow moves to be input for phase start");
    }
    
    private int GetCurrentHealth()
    {
        int currentHealth = 0;
        foreach (Damagable damagePoint in damagePoints)
        {
            currentHealth += damagePoint.health;
        }
        return currentHealth;
    }

    public void SetHealthBar(Slider healthBar)
    {
        this.healthBar = healthBar;
    }

    private void UpdateHealthBar()
    {
        healthBar.value = GetCurrentHealth() / startingHealth;
    }

    public void SetupDamageListeners()
    {
        foreach(Damagable damagePoint in damagePoints)
        {
            damagePoint.damaged += UpdateHealthBar;
        }
    }

    public void RemoveDamageListeners()
    {
        foreach (Damagable damagePoint in damagePoints)
        {
            damagePoint.damaged -= UpdateHealthBar;
        }
    }

}
