using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is to be instantiated and filled with sprite definitions you plan on using
/// </summary>
[CreateAssetMenu(menuName = "IntronDigital/Sprite Asset Definitions")]
public class SpriteAssetDefinitions : ScriptableObject
{
    public List<TMPro.TMP_SpriteAsset> spriteAssets;
}
