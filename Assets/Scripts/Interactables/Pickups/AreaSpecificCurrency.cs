using IntronDigital;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// When Picked up adds level specific currency to inventory
//Based on Star from deadline
public class AreaSpecificCurrency : PickableItem
{
    public int CurrencyID;

    private void OnEnable()
    {
        string areaName = ExpandedLevelManager.Instance.areaName;
        GameScene? scene = ProgressManager.Instance.Scenes.Where(scene => scene.SceneName == areaName).FirstOrDefault();
        if (scene != null && scene.CollectedCurrency.Length != 0)
        {
            if (scene.CollectedCurrency[CurrencyID])
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Triggered when something collides with the star
    /// </summary>
    /// <param name="collider">Other.</param>
    protected override void Pick(GameObject picker)
    {
        // we send a new star event for anyone to catch 
        AreaCurrencyEvent.Trigger(CurrencyID);
    }
}
public struct AreaCurrencyEvent
{
    public int CurrencyID;

    public AreaCurrencyEvent(int currencyID)
    {
        CurrencyID = currencyID;
    }

    static AreaCurrencyEvent e;
    public static void Trigger(int currencyID)
    {
        e.CurrencyID = currencyID;
        MMEventManager.TriggerEvent(e);
    }
}
