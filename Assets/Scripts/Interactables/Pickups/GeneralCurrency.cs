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
public class GeneralCurrency : PickableItem
{
    [SerializeField]
    private int currencyValue = 1;

    protected override void Pick(GameObject picker)
    {
        ProgressManager.Instance.AddToHeldCurrency(currencyValue);
    }
}
