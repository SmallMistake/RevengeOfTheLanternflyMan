using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandleAbilitiesOnPlayerController : MonoBehaviour
{
    public Weapon weapon;

    public void PreventOtherWeapon()
    {
        CharacterHandleWeapon[] characterHandleWeapon = transform.parent.transform.parent.transform.parent.transform.parent.GetComponents<CharacterHandleWeapon>();
        CharacterHandleWeapon weaponHandler = characterHandleWeapon.Where(handler => handler.CurrentWeapon != weapon).First();
        weaponHandler.PermitAbility(false);
    }

    public void AllowOtherWeapon()
    {
        CharacterHandleWeapon[] characterHandleWeapon = transform.parent.transform.parent.transform.parent.transform.parent.GetComponents<CharacterHandleWeapon>();
        CharacterHandleWeapon weaponHandler = characterHandleWeapon.Where(handler => handler.CurrentWeapon != weapon).First();
        weaponHandler.PermitAbility(true);
    }
}
