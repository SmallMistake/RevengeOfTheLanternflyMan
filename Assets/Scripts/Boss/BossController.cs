using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{

    public List<EntityPhaseController> phases;
    public int currentPhaseIndex;

    private GameObject healthBar;
    public string bossName;
    public Health health; //Later on allow each phase to have different health;

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
        healthBar = (GameObject)Instantiate(Resources.Load("UI/PF_BossCanvas_UI"));
        healthBar.GetComponent<BossHealthBarHUDController>().SetUpVisuals(bossName, health);
        //mmHealthBar.TargetProgressBar = healthBar.GetComponent<MMProgressBar>();
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
        if(phases.Count > 0)
        {
            phases[currentPhaseIndex].StartPhase(healthBar.GetComponentInChildren<Slider>(), this);
        }
        else
        {
            print("TODO Add Phases to Boss Controller " + gameObject.name);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
