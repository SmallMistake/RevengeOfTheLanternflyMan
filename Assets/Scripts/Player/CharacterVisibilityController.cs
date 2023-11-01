using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Tooltip("This controller controls how visible the character is to other entities")]
public class CharacterVisibilityController : CharacterAbility
{
    [SerializeField]
    private List<StealthArea> stealthAreasIn = new List<StealthArea>();
    protected override void Initialization()
    {
        base.Initialization();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StealthArea stealthArea = collision.gameObject.GetComponent<StealthArea>();
        if(stealthArea != null) { 
            if(!stealthAreasIn.Contains(stealthArea))
            {
                stealthAreasIn.Add(stealthArea);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StealthArea stealthArea = collision.gameObject.GetComponent<StealthArea>();
        if (stealthArea != null)
        {
            if (stealthAreasIn.Contains(stealthArea))
            {
                stealthAreasIn.Remove(stealthArea);
            }
        }
    }
}
