using IntronDigital;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyRowController : MonoBehaviour
{
    public string currencyAreaName;
    public TextMeshProUGUI amountTextMesh;
    public GameObject iconObject;

    private void OnEnable()
    {
        GameScene[] scenes = ProgressManager.Instance.scenes;
        bool foundCurrency = false;
        foreach (GameScene scene in scenes)
        {
            if (scene.SceneName == currencyAreaName)
            {
                Setup(scene);
                foundCurrency = true;
                iconObject.SetActive(true);
                break;
            }
        }
        if (!foundCurrency)
        {
            iconObject.SetActive(false);
            amountTextMesh.text = "";
        }
    }
    public void Setup(GameScene scene)
    {
        amountTextMesh.text = $"{scene.CollectedCurrencyCount}/{scene.CollectedCurrency.Length}";
    }
}
