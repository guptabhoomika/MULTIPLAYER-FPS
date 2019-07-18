
using UnityEngine;
using UnityEngine.Networking;

public class playerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private playerWeapon weapon;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private GameObject weaponGFX;
    [SerializeField]
    private string WeaponLayerName = "Weapon";

    private void Start()
    {
        if(cam==null)
        {
            Debug.LogError("Camera missing");
            this.enabled = false;
        }

        weaponGFX.layer = LayerMask.NameToLayer(WeaponLayerName);
        
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }
    [Client]
    void shoot()
    {
        RaycastHit _hit;


        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
           if(_hit.collider.tag== PLAYER_TAG)
            {
                CmdPlayershoot(_hit.collider.name , weapon.damage);
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
