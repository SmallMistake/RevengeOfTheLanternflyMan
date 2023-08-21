using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HiddenDuplicateController : MonoBehaviour
{
    public SpriteRenderer rendererToDuplicate;
    public SpriteRenderer rendererToWriteTo;

    //public float offsetDistance = 0.7f;
    //public float checkDistance = 0.1f;
    private void OnEnable()
    {
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        rendererToWriteTo.transform.position = rendererToDuplicate.transform.position;
        rendererToWriteTo.transform.rotation = rendererToDuplicate.transform.rotation;
        rendererToWriteTo.transform.localScale = rendererToDuplicate.transform.lossyScale;
        rendererToWriteTo.sprite = rendererToDuplicate.sprite;
    }
    /*

    private bool CheckIfObjectIsVisible()
    {
        List<Vector3> directions = new List<Vector3>() {
            rendererToDuplicate.transform.position + new Vector3(0, -offsetDistance, 0),
            rendererToDuplicate.transform.position + new Vector3(0, -offsetDistance, 0),
            rendererToDuplicate.transform.position + new Vector3(0, -offsetDistance, 0),
            rendererToDuplicate.transform.position + new Vector3(0, -offsetDistance, 0)
        };
        bool covered = false;
        foreach (Vector3 direction in directions)
        {
            RaycastHit2D[] hitList = Physics2D.RaycastAll(rendererToDuplicate.transform.position + new Vector3(0, -offsetDistance, 0), -Vector2.up, distance: checkDistance);
            Debug.DrawRay(rendererToDuplicate.transform.position + new Vector3(0, offsetDistance, 0), -Vector2.up * checkDistance, Color.magenta);
            CheckIfCovered(hitList);
        }


        //bool upIsCovered =  CheckIfCovered(hitList);

        return true;
    }

    private bool CheckIfCovered(RaycastHit2D[] hitList)
    {
        foreach (RaycastHit2D hit in hitList)
        {
            // If it hits something...
            if (hit.collider != null)
            {
                if (LayerMask.LayerToName(hit.collider.gameObject.layer).Contains("2F"))
                {
                    print(hit.collider.name + " " + LayerMask.LayerToName(hit.collider.gameObject.layer));
                    return true;
                }
            }
        }

        return false;
    }
    */
}
