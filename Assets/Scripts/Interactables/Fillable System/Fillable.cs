using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Filler {
    public string fillerName;
    public Sprite fillerSprite;
}

/// <summary>
/// A fillable can be used to collect liquids
/// </summary>
public class Fillable : MonoBehaviour
{
    [Tooltip("Fillers that can fill this fillable")] //TODO look into changing this to a better system, in the future
    [SerializeField]
    private List<Filler> fillers;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    [Tooltip("What is currently filling the fillable")]
    private string currentFiller;

    [Header("Feedbacks")]
    [SerializeField]
    protected MMF_Player pourOutFeedbacks;

    /// <summary>
    /// When entering an area that has a container filler, fill the fillable
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ContainerFiller filler = collision.GetComponent<ContainerFiller>();
        if(filler != null) {
            ChangeFillStatus(filler.fillName);
        }
    }

    /// <summary>
    /// When entering an area that has a container filler, fill the fillable
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContainerFiller filler = collision.gameObject.GetComponent<ContainerFiller>();
        if (filler != null)
        {
            ChangeFillStatus(filler.fillName);
        }
    }

    /// <summary>
    /// Use whatever is being held by the fillable
    /// </summary>
    /// <param name="collision"></param>
    public void PourOutContents(List<GameObject> objectsInRange)
    {
        if (currentFiller != "" && currentFiller.ToLower() != "empty")
        {
            pourOutFeedbacks?.PlayFeedbacks();
            foreach (GameObject obj in objectsInRange)
            {
                Fillable fillable = obj.GetComponent<Fillable>();
                fillable?.ChangeFillStatus(currentFiller);
                /* Optional way to only fill one fillable in range if desired in the future
                if (fillable != null)
                {
                    fillable.ChangeFillStatus(currentFiller);
                    break;
                }
                */
            }
            ChangeFillStatus("empty");
        }
    }


    public void ChangeFillStatus(string fillName)
    {
        Filler fillerSprite = fillers.Where(fillerPresets => fillerPresets.fillerName.ToLower() == fillName.ToLower()).FirstOrDefault();
        if (fillerSprite != null)
        {
            spriteRenderer.sprite = fillerSprite.fillerSprite;
            currentFiller = fillerSprite.fillerName;
        }
    }
}
