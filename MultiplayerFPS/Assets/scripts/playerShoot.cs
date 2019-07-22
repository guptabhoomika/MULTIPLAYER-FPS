
using UnityEngine;
using UnityEngine.Networking;  


[RequireComponent(typeof(weaponManager))]


public class playerShoot : NetworkBehaviour
{
    [SerializeField]
    private const string PLAYER_TAG = "Player";
    [SerializeField]
    private Camera cam;
    
    private playerWeapon currentweapon;
    [SerializeField]
    private LayerMask mask;

    private weaponManager WeaponManager;
   
    private void Start()
    {
        if(cam==null)
        {
            Debug.LogError("Camera missing");
            this.enabled = false;
        }

        WeaponManager = GetComponent<weaponManager>();



    }
    private void Update()
    {
        currentweapon = WeaponManager.getCurrentWeapon();
        if(currentweapon.firerate <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                shoot();
            }

        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("shoot", 0f, 1f / currentweapon.firerate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("shoot");
            }
        }


    }

    [Command]
    void CmdOnShoot()
    {
        RpcDoShootEffect();
    }

    [ClientRpc]
    void RpcDoShootEffect()
    {
        WeaponManager.getCurrentWeaponGfx().MuzzleFlask.Play();
    }
    [Command]
    void CmdonHit(Vector3 _pos , Vector3 _normal)
    {
        RpcOnHitEffect(_pos, _normal);
    }

    [ClientRpc]
    void RpcOnHitEffect(Vector3 _pos , Vector3 _normal)
    {

        GameObject _hitEffect =(GameObject)Instantiate(WeaponManager.getCurrentWeaponGfx().hitEffectPrefab, _pos, Quaternion.LookRotation(_normal));
        Destroy(_hitEffect, 2f);
    }


    [Client]
    void shoot()
    {

        if(!isLocalPlayer)
        {
            return;
        }

        CmdOnShoot();
        
        RaycastHit _hit;


        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentweapon.range, mask))
        {
           if(_hit.collider.tag== PLAYER_TAG)
            {
                CmdPlayershoot(_hit.collider.name , currentweapon.damage);
            }
            CmdonHit(_hit.point, _hit.normal);
        }

    }
    [Command]
    void CmdPlayershoot(string _playerID , int _damage)
    {
        Debug.Log(_playerID + "has been shot");

        Player _player = gameManager.getPlayer(_playerID);
        _player.RpcTakeDamage(_damage);


    
    }
}
