using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarHUD : MonoBehaviour,
    MMEventListener<HealthChangeEvent>
{
    public TagMask tagMask = new TagMask();
    public GameObject heartPrefab;

    public Transform rowOne;
    public Transform rowTwo;
    public int numberOfHeartsPerRow;

    public List<Sprite> heartPhases;

    //Each phase is one point
    private List<Image> heartsList;

    public void OnMMEvent(HealthChangeEvent healthChangeInfo)
    {
        if (tagMask.IsInTagMask(healthChangeInfo.AffectedHealth.gameObject.tag))
        {
            if (heartsList == null)
            {
                buildHeartsList(healthChangeInfo.AffectedHealth.MaximumHealth);
            }
            updateCurrentDisplay(healthChangeInfo);
        }
    }

    private void updateCurrentDisplay(HealthChangeEvent healthChangeInfo)
    {
        int currentNumberOfFullHearts = (int)(healthChangeInfo.NewHealth / (heartPhases.Count - 1));
        int currentRemainderOfHearts = (int) healthChangeInfo.NewHealth % (heartPhases.Count  - 1);
        foreach (Image heart in heartsList)
        {
            if(currentNumberOfFullHearts > 0)
            {
                currentNumberOfFullHearts--;
                heart.sprite = heartPhases[0];
            }
            else if(currentRemainderOfHearts > 0)
            {
                heart.sprite = heartPhases[currentRemainderOfHearts];
                currentRemainderOfHearts = 0;
            }
            else
            {
                heart.sprite = heartPhases[heartPhases.Count - 1];
            }
        }
    }

    public void buildHeartsList(float maximumHealth)
    {
        heartsList = new List<Image>();
        float numberOfHearts = maximumHealth / (heartPhases.Count - 1);
        int rowIndex = 0;
        for(int i = 0; i < numberOfHearts; i++)
        {
            GameObject createdHeart = Instantiate(heartPrefab);
            if(rowIndex < numberOfHeartsPerRow)
            {
                createdHeart.transform.SetParent(rowOne.transform);
            }
            else
            {
                createdHeart.transform.SetParent(rowTwo.transform);
            }
            createdHeart.transform.localScale = new Vector3(1, 1, 1);
            heartsList.Add(createdHeart.GetComponent<Image>());
            rowIndex++;
        }
    }

    private void OnEnable()
    {
        this.MMEventStartListening<HealthChangeEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<HealthChangeEvent>();
    }
}
