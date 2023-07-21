using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{

    public List<EntityPhaseController> phases;
    public int currentPhaseIndex;

    private GameObject healthBar;

    private void OnEnable()
    {
        DisplayHUD();
        StartFight();
    }

    private void OnDisable()
    {
        if (healthBar)
        {
            Destroy(healthBar);
        }
    }

    private void DisplayHUD()
    {
        healthBar = (GameObject)Instantiate(Resources.Load("UI/Boss Canvas"));
    }

    private void StartFight()
    {
        currentPhaseIndex = 0;
        StartPhase();
    }

    public void MoveToNextPhase()
    {
        currentPhaseIndex++;
        if (currentPhaseIndex < phases.Count)
        {
            StartPhase();
        }
        else
        {
            Die();
        }
    }

    public void StartPhase()
    {
        phases[currentPhaseIndex].StartPhase(healthBar.GetComponentInChildren<Slider>(), this);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
