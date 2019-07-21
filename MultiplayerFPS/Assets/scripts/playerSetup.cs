
using UnityEngine;
using UnityEngine.Networking;
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(playercontroller))]

public class playerSetup : NetworkBehaviour
{
   
    [SerializeField]
    Behaviour[] componenetsToDisable;
    [SerializeField]
    string layerToname = "RemotePlayer";
    [SerializeField]
    string DontDrawLayerName = "DontDraw";
    [SerializeField]
    GameObject playerGraphics;
    [SerializeField]
    GameObject playerUIPrefab;
    [HideInInspector]
    public GameObject playerUI;


   
    private void Start()
    {
        if(!isLocalPlayer)
        {
            DisableComponenets();
            ChangeLayer();

           
        }
        else
        {
           
            setLayerRecurssive(playerGraphics, LayerMask.NameToLayer(DontDrawLayerName));

           playerUI=  Instantiate(playerUIPrefab);
            playerUI.name = playerUIPrefab.name;
            PlayerUI ui = playerUI.GetComponent<PlayerUI>();
            if(ui == null)
            {
                Debug.LogError("No UI Attached");
            }
            ui.SetPlayerController(GetComponent<playercontroller>());

            GetComponent<Player>().Setup();
        }


      

        
       
    }
    private void setLayerRecurssive(GameObject obj , int newLayer)
    {
        obj.layer = newLayer;

        foreach(Transform child in obj.transform)
        {
            setLayerRecurssive(child.gameObject, newLayer);
        }


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
        Destroy(playerUI);
        if (isLocalPlayer)
        {
            gameManager.instance.SetCameraActive(true);

        }

        gameManager.DergisterPlayer(transform.name);
    }
}
