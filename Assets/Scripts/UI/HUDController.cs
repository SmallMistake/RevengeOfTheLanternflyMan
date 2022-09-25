using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public GameObject heartHolder;
    public TextMeshProUGUI acornText;
    public Sprite emptyHeart;
    public Sprite halfHeart;
    public Sprite fullHeart;

    private List<Image> hearts;

    // Start is called before the first frame update
    private void Awake()
    {
        hearts = new List<Image>();
        foreach (Transform heart in heartHolder.transform)
        {
            hearts.Add(heart.GetComponent<Image>());
        }

        PlayerHealth.playerHealthChanged += UpdateHealthUI;
        PlayerInventory.acornsChanged += UpdateAcornUI;
    }

    private void OnDestroy()
    {
        PlayerHealth.playerHealthChanged -= UpdateHealthUI;
        PlayerInventory.acornsChanged -= UpdateAcornUI;
    }

    private void UpdateHealthUI(int health)
    {
        int healthRemaining = health;
        foreach (Image heart in hearts)
        {
            if(healthRemaining >= 2)
            {
                heart.sprite = fullHeart;
            } else if(healthRemaining >= 1)
            {
                heart.sprite = halfHeart;
            }
            else
            {
                heart.sprite = emptyHeart;
            } 
            healthRemaining -= 2;
        }
    }

    private void UpdateAcornUI(int acorn)
    {
        acornText.text = $"x {acorn}";
    }
}