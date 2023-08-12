using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "IntronDigital/Advanced Rule Tile")]
public class AdvancedRuleTile : RuleTile<AdvancedRuleTile.Neighbor>
{
    public bool alwaysConnect;
    public TileBase[] tilesToConnectPrimary;
    public TileBase[] tilesToConnectSecondary;
    public TileBase[] tilesToConnectThird;
    public bool checkSelf;

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Any = 3;
        public const int Nothing = 4;
        public const int Primary_Specified = 5;
        public const int Secondary_Specified = 6;
        public const int Third_Specified = 7;
    }

    public override bool RuleMatch(int neighbor, TileBase other)
    {
        switch (neighbor)
        {
            case Neighbor.This: return Check_This(other);
            case Neighbor.NotThis: return Check_NotThis(other);
            case Neighbor.Any: return Check_Any(other);
            case Neighbor.Nothing: return Check_Nothing(other);
            case Neighbor.Primary_Specified: return Check_Primary_Specified(other);
            case Neighbor.Secondary_Specified: return Check_Secondary_Specified(other);
            case Neighbor.Third_Specified: return Check_Third_Specified(other);
        }
        return base.RuleMatch(neighbor, other);
    }

    bool Check_This(TileBase tile)
    {
        if (!alwaysConnect) return tile = this;
        else return tilesToConnectPrimary.Contains(this) || tilesToConnectSecondary.Contains(this) || tilesToConnectThird.Contains(this) || tile == this;
    }

    bool Check_NotThis(TileBase tile)
    {
        return tile != this;
    }

    bool Check_Any(TileBase tile)
    {
        if (checkSelf) return tile != null;
        else return tile != null && tile != this;
    }


    bool Check_Primary_Specified(TileBase tile)
    {
        return tilesToConnectPrimary.Contains(tile);
    }
    bool Check_Secondary_Specified(TileBase tile)
    {
        return tilesToConnectSecondary.Contains(tile);
    }
    bool Check_Third_Specified(TileBase tile)
    {
        return tilesToConnectThird.Contains(tile);
    }

    bool Check_Nothing(TileBase tile)
    {
        return tile == null;
    }

}
