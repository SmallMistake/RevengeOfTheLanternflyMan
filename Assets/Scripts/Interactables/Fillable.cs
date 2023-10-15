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


public class Fillable : MonoBehaviour
{

    [SerializeField]
    private List<Filler> fillers;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private string currentFiller;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ContainerFiller filler = collision.GetComponent<ContainerFiller>();
        if(filler != null) {
            ChangeFillStatus(filler.fillName);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContainerFiller filler = collision.gameObject.GetComponent<ContainerFiller>();
        if (filler != null)
        {
            ChangeFillStatus(filler.fillName);
        }
    }

    public void PourOutContents(List<GameObject> objectsInRange)
    {
        foreach(GameObject obj in objectsInRange)
        {
            Fillable fillable = obj.GetComponent<Fillable>();
            if(fillable != null)
            {
                fillable.ChangeFillStatus(currentFiller);
                break;
            }
        }
        ChangeFillStatus("empty");
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
