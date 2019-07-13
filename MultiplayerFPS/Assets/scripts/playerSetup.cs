
using UnityEngine;
using UnityEngine.Networking;
[RequireComponent(typeof(Player))]

public class playerSetup : NetworkBehaviour
{
   
    [SerializeField]
    Behaviour[] componenetsToDisable;
    [SerializeField]
    string layerToname = "RemotePlayer";
    Camera sceneCamera;
    private void Start()
    {
        if(!isLocalPlayer)
        {
            DisableComponenets();
            ChangeLayer();

           
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);

            }
               
        }

        GetComponent<Player>().Setup();

        
       
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        string _netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        gameManager.RegisterPlayer(_netId, _player);
    }
    void DisableComponenets()
    {
        for (int i = 0; i < componenetsToDisable.Length; i++)

        {
            componenetsToDisable[i].enabled = false;
        }
    }
    void ChangeLayer()
    {

        gameObject.layer = LayerMask.NameToLayer(layerToname);


    }
    private void OnDisable()
    {
        if(sceneCamera!= null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
        gameManager.DergisterPlayer(transform.name);
    }
}
