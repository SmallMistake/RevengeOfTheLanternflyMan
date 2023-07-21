using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityPhaseController : MonoBehaviour
{

    public List<Damagable> damagePoints;
    private int startingHealth;

    private Slider healthBar;

    BossController controller;

    public List<AttackController> attackControllers;


    public void StartPhase(Slider healthBar, BossController controller)
    {
        this.healthBar = healthBar;
        this.controller = controller;
        startingHealth = GetCurrentHealth();
        StartAttackControllers();
        SetupDamageListeners();
        UpdateHealthBar();
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

    private void UpdateHealthBar()
    {
        healthBar.value = (float) GetCurrentHealth() / (float) startingHealth;
        if(healthBar.value <= 0 )
        {
            controller.MoveToNextPhase();
        }
    }

    private void StartAttackControllers()
    {
        foreach(AttackController attackController in attackControllers)
        {
            attackController.StartAttacks();
        }
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
