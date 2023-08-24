using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarHUDController : MonoBehaviour
{
    public TextMeshProUGUI bossNameTextMesh;
    public Health healthToListenTo;
    public Slider healthBar;

    public void SetUpVisuals(string bossName, Health healthToListenTo)
    {
        bossNameTextMesh.text = bossName;
        this.healthToListenTo = healthToListenTo;
        //Look into Shared Health Section of Health for multiple damage nodes.
        healthToListenTo.OnHit += HandleHealthChange;
        HandleHealthChange();
    }

    void HandleHealthChange()
    {
        healthBar.value = healthToListenTo.CurrentHealth / healthToListenTo.MaximumHealth;
    }
}
