using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI acornText;
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerHealth.playerHealthChanged += UpdateHealthUI;
        PlayerInventory.acornsChanged += UpdateAcornUI;
    }

    private void UpdateHealthUI(int health)
    {
        healthText.text = $"Health: {health}";
    }

    private void UpdateAcornUI(int acorn)
    {
        acornText.text = $"Acorn: {acorn}";
    }
}