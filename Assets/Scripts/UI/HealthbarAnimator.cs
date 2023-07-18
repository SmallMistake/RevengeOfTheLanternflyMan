using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarAnimator : MonoBehaviour
{
    Slider healthBar;
    private void Start()
    {
        healthBar = GetComponent<Slider>();
    }
    public void UpdateVisua()
    {
        if(healthBar.value < 0.2)
        {
            healthBar.colo
        }
    }
}
