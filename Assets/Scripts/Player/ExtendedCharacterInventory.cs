using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedCharacterInventory : CharacterInventory
{
    /// the name of the inventory where this character stores weapons
    [Tooltip("the name of the inventory where this character stores secondary weapons")]
    public string SecondaryWeaponInventoryName;

    public Inventory SecondaryWeaponInventory { get; set; }

    /// the target handle weapon ability - if left empty, will pick the first one it finds
    [Tooltip("the target handle weapon ability - if left empty, will pick the first one it finds")]
    public CharacterHandleWeapon SecondaryCharacterHandleWeapon;

    bool _nextFrameSecondaryWeapon = false;


    /// <summary>
    /// Grabs all inventories, and fills weapon lists
    /// </summary>

    protected override void Setup()
    {
        if (InventoryTransform == null)
        {
            InventoryTransform = this.transform;
        }
        GrabInventories();
        if (CharacterHandleWeapon == null)
        {
            CharacterHandleWeapon = _character?.FindAbility<CharacterHandleWeapon>();
        }
        else if (SecondaryCharacterHandleWeapon == null)
        {
            SecondaryCharacterHandleWeapon = _character?.FindAbility<CharacterHandleSecondaryWeapon>();
        }
        FillAvailableWeaponsLists();

        if (_initialized)
        {
            return;
        }

        bool mainInventoryEmpty = true;
        if (MainInventory != null)
        {
            mainInventoryEmpty = MainInventory.NumberOfFilledSlots == 0;
        }
        bool canAutoPick = !(AutoPickOnlyIfMainInventoryIsEmpty && !mainInventoryEmpty);
        bool canAutoEquip = !(AutoEquipOnlyIfMainInventoryIsEmpty && !mainInventoryEmpty);

        // we auto pick items if needed
        if ((AutoPickItems.Length > 0) && !_initialized && canAutoPick)
        {
            foreach (AutoPickItem item in AutoPickItems)
            {
                MMInventoryEvent.Trigger(MMInventoryEventType.Pick, null, item.Item.TargetInventoryName, item.Item, item.Quantity, 0, PlayerID);
            }
        }

        // we auto equip a weapon if needed
        if ((AutoEquipWeaponOnStart != null) && !_initialized && canAutoEquip)
        {
            MMInventoryEvent.Trigger(MMInventoryEventType.Pick, null, AutoEquipWeaponOnStart.TargetInventoryName, AutoEquipWeaponOnStart, 1, 0, PlayerID);
            EquipWeapon(AutoEquipWeaponOnStart.ItemID);
        }
        _initialized = true;
    }

    public override void ProcessAbility()
    {
        base.ProcessAbility();

        if (_nextFrameWeapon)
        {
            EquipWeapon(_nextFrameWeaponName);
            _nextFrameWeapon = false;
        }
    }

    /// <summary>
    /// Grabs any inventory it can find that matches the names set in the inspector
    /// </summary>
    protected override void GrabInventories()
    {
        Inventory[] inventories = FindObjectsOfType<Inventory>();
        foreach (Inventory inventory in inventories)
        {
            if (inventory.PlayerID != PlayerID)
            {
                continue;
            }
            if ((MainInventory == null) && (inventory.name == MainInventoryName))
            {
                MainInventory = inventory;
            }
            if ((WeaponInventory == null) && (inventory.name == WeaponInventoryName))
            {
                WeaponInventory = inventory;
            }
            if ((SecondaryWeaponInventory == null) && (inventory.name == SecondaryWeaponInventoryName))
            {
                SecondaryWeaponInventory = inventory;
            }
            if ((HotbarInventory == null) && (inventory.name == HotbarInventoryName))
            {
                HotbarInventory = inventory;
            }
        }
        if (MainInventory != null) { MainInventory.SetOwner(this.gameObject); MainInventory.TargetTransform = InventoryTransform; }
        if (WeaponInventory != null) { WeaponInventory.SetOwner(this.gameObject); WeaponInventory.TargetTransform = InventoryTransform; }
        if (SecondaryWeaponInventory != null) { SecondaryWeaponInventory.SetOwner(this.gameObject); SecondaryWeaponInventory.TargetTransform = InventoryTransform; }
        if (HotbarInventory != null) { HotbarInventory.SetOwner(this.gameObject); HotbarInventory.TargetTransform = InventoryTransform; }
    }

    /// <summary>
    /// On handle input, we watch for the switch weapon button, and switch weapon if needed
    /// </summary>
    protected override void HandleInput()
    {
        if (!AbilityAuthorized
            || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal))
        {
            return;
        }
        if (_inputManager.SwitchWeaponButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
        {
            SwitchWeapon();
        }
    }

    /// <summary>
    /// Fills the weapon list. The weapon list will be used to determine what weapon we can switch to
    /// </summary>
    protected override void FillAvailableWeaponsLists()
    {
        _availableWeaponsIDs = new List<string>();
        if ((CharacterHandleWeapon == null) || (WeaponInventory == null))
        {
            return;
        }
        _availableWeapons = MainInventory.InventoryContains(ItemClasses.Weapon);
        foreach (int index in _availableWeapons)
        {
            _availableWeaponsIDs.Add(MainInventory.Content[index].ItemID);
        }
        if (!InventoryItem.IsNull(WeaponInventory.Content[0]))
        {
            _availableWeaponsIDs.Add(WeaponInventory.Content[0].ItemID);
        }
        else if (!InventoryItem.IsNull(SecondaryWeaponInventory.Content[0]))
        {
            _availableWeaponsIDs.Add(SecondaryWeaponInventory.Content[0].ItemID);
        }

        _availableWeaponsIDs.Sort();
    }

    /// <summary>
    /// Determines the name of the next weapon in line
    /// </summary>
    protected override void DetermineNextWeaponName()
    {
        if (InventoryItem.IsNull(WeaponInventory.Content[0]))
        {
            _nextWeaponID = _availableWeaponsIDs[0];
            return;
        }

        if ((_nextWeaponID == _emptySlotWeaponName) || (_nextWeaponID == _initialSlotWeaponName))
        {
            _nextWeaponID = _availableWeaponsIDs[0];
            return;
        }

        for (int i = 0; i < _availableWeaponsIDs.Count; i++)
        {
            if (_availableWeaponsIDs[i] == WeaponInventory.Content[0].ItemID)
            {
                if (i == _availableWeaponsIDs.Count - 1)
                {
                    switch (WeaponRotationMode)
                    {
                        case WeaponRotationModes.AddEmptySlot:
                            _nextWeaponID = _emptySlotWeaponName;
                            return;
                        case WeaponRotationModes.AddInitialWeapon:
                            _nextWeaponID = _initialSlotWeaponName;
                            return;
                    }

                    _nextWeaponID = _availableWeaponsIDs[0];
                }
                else
                {
                    _nextWeaponID = _availableWeaponsIDs[i + 1];
                }
            }
        }
    }

    /// <summary>
    /// Equips the weapon with the name passed in parameters
    /// </summary>
    /// <param name="weaponID"></param>
    public override void EquipWeapon(string weaponID)
    {
        if ((weaponID == _emptySlotWeaponName) && (CharacterHandleWeapon != null))
        {
            MMInventoryEvent.Trigger(MMInventoryEventType.UnEquipRequest, null, WeaponInventoryName, WeaponInventory.Content[0], 0, 0, PlayerID);
            CharacterHandleWeapon.ChangeWeapon(null, _emptySlotWeaponName, false);
            MMInventoryEvent.Trigger(MMInventoryEventType.Redraw, null, WeaponInventory.name, null, 0, 0, PlayerID);
            return;
        }

        if ((weaponID == _initialSlotWeaponName) && (CharacterHandleWeapon != null))
        {
            MMInventoryEvent.Trigger(MMInventoryEventType.UnEquipRequest, null, WeaponInventoryName, WeaponInventory.Content[0], 0, 0, PlayerID);
            CharacterHandleWeapon.ChangeWeapon(CharacterHandleWeapon.InitialWeapon, _initialSlotWeaponName, false);
            MMInventoryEvent.Trigger(MMInventoryEventType.Redraw, null, WeaponInventory.name, null, 0, 0, PlayerID);
            return;
        }

        for (int i = 0; i < MainInventory.Content.Length; i++)
        {
            if (InventoryItem.IsNull(MainInventory.Content[i]))
            {
                continue;
            }
            if (MainInventory.Content[i].ItemID == weaponID)
            {
                if (_nextFrameSecondaryWeapon)
                {
                    (MainInventory.Content[i] as InventoryWeapon).HandleWeaponID = 2;
                    (MainInventory.Content[i] as InventoryWeapon).TargetEquipmentInventoryName = SecondaryWeaponInventoryName;
                }
                MMInventoryEvent.Trigger(MMInventoryEventType.EquipRequest, null, MainInventory.name, MainInventory.Content[i], 0, i, PlayerID);
                break;
            }
        }
    }

    /// <summary>
    /// Switches to the next weapon in line
    /// </summary>
    protected override void SwitchWeapon()
    {
        // if there's no character handle weapon component, we can't switch weapon, we do nothing and exit
        if ((CharacterHandleWeapon == null) || (WeaponInventory == null))
        {
            return;
        }

        FillAvailableWeaponsLists();

        // if we only have 0 or 1 weapon, there's nothing to switch, we do nothing and exit
        if (_availableWeaponsIDs.Count <= 0)
        {
            return;
        }

        DetermineNextWeaponName();
        EquipWeapon(_nextWeaponID);
        PlayAbilityStartFeedbacks();
        PlayAbilityStartSfx();
    }

    /// <summary>
    /// Watches for InventoryLoaded events
    /// When an inventory gets loaded, if it's our WeaponInventory, we check if there's already a weapon equipped, and if yes, we equip it
    /// </summary>
    /// <param name="inventoryEvent">Inventory event.</param>
    public override void OnMMEvent(MMInventoryEvent inventoryEvent)
    {
        if (inventoryEvent.InventoryEventType == MMInventoryEventType.InventoryLoaded)
        {
            if (inventoryEvent.TargetInventoryName == WeaponInventoryName)
            {
                this.Setup();
                if (WeaponInventory != null)
                {
                    if (!InventoryItem.IsNull(WeaponInventory.Content[0]))
                    {
                        CharacterHandleWeapon.Setup();
                        WeaponInventory.Content[0].Equip(PlayerID);
                    }
                }
            }
            if (inventoryEvent.TargetInventoryName == SecondaryWeaponInventoryName)
            {
                this.Setup();
                if (SecondaryWeaponInventory != null)
                {
                    if (!InventoryItem.IsNull(SecondaryWeaponInventory.Content[0]))
                    {
                        SecondaryCharacterHandleWeapon.Setup();
                        SecondaryWeaponInventory.Content[0].Equip(PlayerID);
                    }
                }
            }
        }
        if (inventoryEvent.InventoryEventType == MMInventoryEventType.Pick)
        {
            bool isSubclass = (inventoryEvent.EventItem.GetType().IsSubclassOf(typeof(InventoryWeapon)));
            bool isClass = (inventoryEvent.EventItem.GetType() == typeof(InventoryWeapon));
            if (isClass || isSubclass)
            {
                InventoryWeapon inventoryWeapon = (InventoryWeapon)inventoryEvent.EventItem;
                switch (inventoryWeapon.AutoEquipMode)
                {
                    case InventoryWeapon.AutoEquipModes.NoAutoEquip:
                        // we do nothing
                        break;

                    case InventoryWeapon.AutoEquipModes.AutoEquip:
                        _nextFrameWeapon = true;
                        _nextFrameWeaponName = inventoryEvent.EventItem.ItemID;
                        break;

                    case InventoryWeapon.AutoEquipModes.AutoEquipIfEmptyHanded:
                        if (CharacterHandleWeapon.CurrentWeapon == null)
                        {
                            _nextFrameWeapon = true;
                            _nextFrameSecondaryWeapon = false;
                            _nextFrameWeaponName = inventoryEvent.EventItem.ItemID;
                        }
                        else if (SecondaryCharacterHandleWeapon.CurrentWeapon == null)
                        {
                            for (int i = 0; i < MainInventory.Content.Length; i++)
                            {
                                if (InventoryItem.IsNull(MainInventory.Content[i]))
                                {
                                    continue;
                                }
                                if (MainInventory.Content[i].ItemID == inventoryEvent.EventItem.ItemID)
                                {
                                    break;
                                }
                            }
                            _nextFrameWeapon = true;
                            _nextFrameSecondaryWeapon = true;
                            _nextFrameWeaponName = inventoryEvent.EventItem.ItemID;
                        }
                        break;
                }
            }
        }
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        if (WeaponInventory != null)
        {
            MMInventoryEvent.Trigger(MMInventoryEventType.UnEquipRequest, null, WeaponInventoryName, WeaponInventory.Content[0], 0, 0, PlayerID);
        }
    }
}
