using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead = false;
    public bool isdead
    {
        get {
            return _isDead;
        }
        protected set {
            _isDead = value;
        }

    }
    [SerializeField]
    private int maxHealth = 100;
    [SyncVar]
    private int currentHealth;

    [SerializeField]
    private Behaviour[] onDeadDisable;
    private bool[] wasEnabled;

    public void Setup()
    {
        wasEnabled = new bool[onDeadDisable.Length];

        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = onDeadDisable[i].enabled;

        }
   
        SetDefault();
    }
    private void Update()
    {
        if (!isLocalPlayer)
            return;
        if(Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(10000);
        }

           
    }

    [ClientRpc]
    public void RpcTakeDamage(int _damage)
    {
        if (isdead)
            return;
        currentHealth -= _damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {

        isdead = true;
        for (int i = 0; i < onDeadDisable.Length; i++)
        {
            onDeadDisable[i].enabled = false;
            
        }
        Collider _col = GetComponent<Collider>();
        if (_col != null)
        {
            _col.enabled = false;
        }
        Debug.Log("Is dead");
        StartCoroutine(Respawn());

    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(gameManager.instance.matchsettings.respawnTime);

        SetDefault();

        Transform _spawnpoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnpoint.position;
        transform.rotation = _spawnpoint.rotation;

        Debug.Log("respawned");
    }

    public void SetDefault()
    {

        isdead = false;


        currentHealth = maxHealth;


        for (int i = 0; i < onDeadDisable.Length; i++)
        {
            onDeadDisable[i].enabled = wasEnabled[i];
        }
        Collider _col = GetComponent<Collider>();
        if(_col !=  null)
            {
            _col.enabled = true;
        }
          
       

    }
}


