using MoreMountains.InventoryEngine;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedCharacterHandleWeapon : CharacterHandleWeapon
{
    [SerializeField]
    private string inventoryName;

    protected override void Start()
    {
        print("TODO Get Inventory slot on load");
        Inventory inventory = Inventory.FindInventory(inventoryName, "Player1");
        if(inventory != null)
        {
            if(inventory.Content != null && inventory.Content[0] != null)
            {
                inventory.Content[0].Equip("Player1");
            }
        }
        base.Start();
    }
}
