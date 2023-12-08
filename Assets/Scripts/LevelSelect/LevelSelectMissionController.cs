using IntronDigital;
using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMissionController : MonoBehaviour
{
    public string levelName;
    public GameObject button;
    public TextMeshProUGUI levelCollectablesTextMesh;

    void Start()
    {
        SetData();
    }

    private void SetData()
    {
        GameScene sceneData = ProgressManager.Instance.scenes.Where(scene => scene.SceneName == levelName).FirstOrDefault();
        if(sceneData != null)
        {
            if(sceneData.LevelUnlocked == false)
            {
                button.SetActive(false);
            }
            levelCollectablesTextMesh.text = $"{sceneData.CollectedCurrencyCount}/50";
        }
    }

    //This is to be done to allow you to set visuals from the editor
    public void SetupVisuals()
    {
        print("TODO Setup Visuals");
    }
}
