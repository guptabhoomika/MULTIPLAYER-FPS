
using UnityEngine;
using UnityEngine.Networking;  


[RequireComponent(typeof(weaponManager))]


public class playerShoot : NetworkBehaviour
{
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
    [Client]
    void shoot()
    {
        Debug.Log("shoot");
        RaycastHit _hit;


        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentweapon.range, mask))
        {
           if(_hit.collider.tag== PLAYER_TAG)
            {
                CmdPlayershoot(_hit.collider.name , currentweapon.damage);
            }
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
