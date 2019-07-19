using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class weaponManager : NetworkBehaviour
{


    [SerializeField]
    private string WeaponLayerName = "Weapon";

    [SerializeField]
    private playerWeapon primaryWeapon;

    [SerializeField]
    private Transform weaponHolder;

    private playerWeapon currentWeapon;


    void Start()
    {
        EquipWeapon(primaryWeapon);
    }


    public playerWeapon getCurrentWeapon()
    {
        return currentWeapon;
    }


    void EquipWeapon(playerWeapon _weapon)
    {
        currentWeapon = _weapon;
       GameObject weaponIns = (GameObject) Instantiate(_weapon.weaponGfx, weaponHolder.position, weaponHolder.rotation);
        weaponIns.transform.SetParent(weaponHolder);
        if (isLocalPlayer)
            weaponIns.layer = LayerMask.NameToLayer(WeaponLayerName);
    }

   
}
