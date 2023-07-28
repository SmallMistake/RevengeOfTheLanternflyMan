using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public void ChangeSprite(Sprite spriteToChangeTo)
    {
        GetComponent<SpriteRenderer>().sprite = spriteToChangeTo;
    }
}
