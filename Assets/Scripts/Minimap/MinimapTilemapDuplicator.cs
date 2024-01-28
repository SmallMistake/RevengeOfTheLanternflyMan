using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinimapTilemapDuplicator : MonoBehaviour
{
    [SerializeField]
    private Tilemap sourceTilemap;
    [SerializeField]
    private Tilemap destinationTilemap;
    [SerializeField]
    private Tile tileToUse;
    public void DuplicateTileMap()
    {
        ClearTilemap();
            for (int x = sourceTilemap.cellBounds.xMin; x < sourceTilemap.cellBounds.xMax; x++)
            {
                for (int y = sourceTilemap.cellBounds.yMin; y < sourceTilemap.cellBounds.yMax; y++)
                {
                    Vector3 Worldpos = sourceTilemap.CellToWorld(new Vector3Int(x, y, (int)sourceTilemap.transform.position.y));
                    Vector3Int TMPos3 = destinationTilemap.WorldToCell(Worldpos);

                    if (sourceTilemap.GetTile(TMPos3) != null)
                    {

                    destinationTilemap.SetTile(TMPos3, tileToUse);
                    }
                }
            }
    }

    public void ClearTilemap()
    {
        destinationTilemap.ClearAllTiles();
    }
}
