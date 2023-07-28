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

    public Image primaryImage;
    public Image secondaryImage;
    private Animator animator;
    public Sprite pesticideSprite;
    public Sprite slingshotSprite;
    public Sprite failedSprite;


    private List<Image> hearts;

    // Start is called before the first frame update
    private void Awake()
    {
        hearts = new List<Image>();
        animator = GetComponent<Animator>();
        foreach (Transform heart in heartHolder.transform)
        {
            hearts.Add(heart.GetComponent<Image>());
        }

        PlayerHealth.playerHealthChanged += UpdateHealthUI;
        //PlayerInventory.acornsChanged += UpdateAcornUI;
        PlayerInteractionController.UsedPrimary += UpdatePrimaryUI;
        PlayerInteractionController.UsedSecondary += UpdateSecondaryUI;
        InventoryController.changedPrimary += UpdatePrimaryUI;
        InventoryController.changedSecondary += UpdateSecondaryUI;
    }

    private void OnDestroy()
    {
        PlayerHealth.playerHealthChanged -= UpdateHealthUI;
        //PlayerInventory.acornsChanged -= UpdateAcornUI;
        PlayerInteractionController.UsedPrimary -= UpdatePrimaryUI;
        PlayerInteractionController.UsedSecondary -= UpdateSecondaryUI;
        InventoryController.changedPrimary -= UpdatePrimaryUI;
        InventoryController.changedSecondary -= UpdateSecondaryUI;
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

    private void UpdatePrimaryUI(Utils.PermanentUpgrades? itemUsed)
    {
        switch (itemUsed)
        {
            case Utils.PermanentUpgrades.Pecticide:
                animator.SetTrigger("PrimaryVisible");
                primaryImage.sprite = pesticideSprite;
                break;
            default:
                animator.SetTrigger("PrimaryFailed");
                primaryImage.sprite = failedSprite;
                break;
        }
    }

    private void UpdateSecondaryUI(Utils.PermanentUpgrades? itemUsed)
    {
        switch (itemUsed)
        {
            case Utils.PermanentUpgrades.Walnut:
                animator.SetTrigger("SecondaryVisible");
                secondaryImage.sprite = slingshotSprite;
                break;
            default:
                animator.SetTrigger("SecondaryFailed");
                secondaryImage.sprite = failedSprite;
                break;
        }
    }

    private void UpdateAcornUI(int acorn)
    {
        acornText.text = $"x {acorn}";
    }
}