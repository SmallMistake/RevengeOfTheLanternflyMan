using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AdvancedRuleTile))]
[CanEditMultipleObjects]
public class AdvancedRuleTileEditor : RuleTileEditor
{
    public Texture2D any;
    public Texture2D none;

    public override void RuleOnGUI(Rect rect, Vector3Int position, int neighbor)
    {
        switch (neighbor)
        {
            case 3:
                GUI.DrawTexture(rect, any);
                break;
            case 4:
                GUI.DrawTexture(rect, none);
                break;
                /*
            case 5:
                GUI.DrawTexture(rect, arrows[GetArrowIndex(position)]);
                break;
            case 6:
                GUI.DrawTexture(rect, arrows[GetArrowIndex(position)]);
                break;
            case 7:
                GUI.DrawTexture(rect, arrows[GetArrowIndex(position)]);
                break;
                */


        }

        base.RuleOnGUI(rect, position, neighbor);
    }
}
