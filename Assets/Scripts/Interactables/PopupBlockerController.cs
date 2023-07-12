using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PopupBlockerController : MonoBehaviour
{

    public TileBase onSprite;
    public TileBase offSprite;
    public TileBase tempSprite;

    private List<ISwitch> switches = new List<ISwitch>();

    Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform switchTransform in transform)
        {
            ISwitch iSwitch = switchTransform.gameObject.GetComponent<ISwitch>();
            switches.Add(iSwitch);
            iSwitch.GetTriggerEvent().AddListener(HandleStateChange);
        }
        tilemap = GetComponent<Tilemap>();
    }

    public void HandleStateChange(bool changedState)
    {
        foreach (ISwitch iswitch in switches)
        {
            iswitch.SetState(changedState);
        }
        flipTiles();
    }

    private void flipTiles()
    {
        tilemap.SwapTile(onSprite, tempSprite);
        tilemap.SwapTile(offSprite, onSprite);
        tilemap.SwapTile(tempSprite, offSprite);
    }
}
