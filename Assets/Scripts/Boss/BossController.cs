using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    //Replace this with a call that occurs when the room is entered later on.
    public bool firstLoad = true;

    public List<EntityPhaseController> phases;
    public EntityPhaseController currentPhase;

    private GameObject healthBar;

    private void OnEnable()
    {
        if (firstLoad)
        {
            firstLoad = !firstLoad;
        }
        else
        {
            DisplayHUD();
            StartFight();
        }
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
        currentPhase = phases[0];
        currentPhase.SetHealthBar(healthBar.GetComponentInChildren<Slider>());
        currentPhase.StartPhase();
    }
}
