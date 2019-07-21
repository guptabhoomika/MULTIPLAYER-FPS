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
    private weaponGfx currentGfx;


    void Start()
    {
        EquipWeapon(primaryWeapon);
    }


    public playerWeapon getCurrentWeapon()
    {
        return currentWeapon;
    }
    public weaponGfx getCurrentWeaponGfx()
    {
        return currentGfx;
    }


    void EquipWeapon(playerWeapon _weapon)
    {
        currentWeapon = _weapon;
       GameObject weaponIns = (GameObject) Instantiate(_weapon.weaponGfx, weaponHolder.position, weaponHolder.rotation);
        weaponIns.transform.SetParent(weaponHolder);

        currentGfx = weaponIns.GetComponent<weaponGfx>();
        if(currentGfx == null)
        {
            Debug.LogError("Graphics Not Attached");
        }

        if (isLocalPlayer)
            Utility.SetLayerReccursively(weaponIns, LayerMask.NameToLayer(WeaponLayerName));
    }

   
}
