using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(playerSetup))]

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

    [SerializeField]
    private GameObject[] onDeadGameobjectDisable;

    [SerializeField]
    private GameObject deadEffect;

    [SerializeField]
    private GameObject spawnEffect;

    private bool firstSetup = true;

    public void Setup()
    {

        if(isLocalPlayer)
        {
            gameManager.instance.SetCameraActive(true);
            GetComponent<playerSetup>().playerUI.SetActive(true);

        }

        CmdBroadCast();
        





    }


    [Command]
    private void CmdBroadCast()
    {

        RpcSetup();

    }

    [ClientRpc]
    private void RpcSetup()
    {
      
        if(firstSetup)

        {
            wasEnabled = new bool[onDeadDisable.Length];


            for (int i = 0; i < wasEnabled.Length; i++)
            {
                wasEnabled[i] = onDeadDisable[i].enabled;

            }

            firstSetup = false;
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

        for (int i = 0; i < onDeadGameobjectDisable.Length; i++)
        {
            onDeadGameobjectDisable[i].SetActive(false);

        }


        Collider _col = GetComponent<Collider>();
        if (_col != null)
        {
            _col.enabled = false;
        }
        Debug.Log("Is dead");
        GameObject deadIns = (GameObject)Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(deadIns, 3f);

        if(isLocalPlayer)
        {
            gameManager.instance.SetCameraActive(true);
            GetComponent<playerSetup>().playerUI.SetActive(false);
        }

        StartCoroutine(Respawn());

    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(gameManager.instance.matchsettings.respawnTime);

       
        Transform _spawnpoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnpoint.position;
        transform.rotation = _spawnpoint.rotation;


        yield return new WaitForSeconds(0.1f);

        Setup(); 


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

        for (int i = 0; i < onDeadGameobjectDisable.Length; i++)
        {
            onDeadGameobjectDisable[i].SetActive(true);

        }

        Collider _col = GetComponent<Collider>();
        if(_col !=  null)
            {
            _col.enabled = true;
        }

      



        GameObject respawnins= (GameObject)Instantiate(spawnEffect, transform.position, Quaternion.identity);
        Destroy(respawnins, 3f);


    }
}


