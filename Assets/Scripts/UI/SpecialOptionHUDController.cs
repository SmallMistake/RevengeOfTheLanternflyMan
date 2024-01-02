using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This will listen to the players current available special option and then display it on the HUD
/// </summary>
public class SpecialOptionHUDController : MonoBehaviour,
    MMEventListener<SpecialUseChangeEvent>,
    MMEventListener<SpecialUsedEvent>
{
    [SerializeField]
    TextMeshProUGUI textMesh;
    [SerializeField]
    MMFeedbacks ButtonPressedFeedbacks;

    private void OnEnable()
    {
        this.MMEventStartListening<SpecialUseChangeEvent>();
        this.MMEventStartListening<SpecialUsedEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<SpecialUseChangeEvent>();
        this.MMEventStopListening<SpecialUsedEvent>();
    }

    public void OnMMEvent(SpecialUseChangeEvent eventObject)
    {
        textMesh.text = eventObject.NewCurrentAbility.friendlyName;
    }

    public void OnMMEvent(SpecialUsedEvent eventObject)
    {
        ButtonPressedFeedbacks?.PlayFeedbacks();
    }
}
