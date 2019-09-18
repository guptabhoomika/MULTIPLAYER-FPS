using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class hostGame : NetworkBehaviour
{
    [SerializeField]
    private uint roomSize = 5;

    private string roomName;

    private int requestDomain;

    private int eloScore;

    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
        if(networkManager.enabled)
        {
            networkManager.StartMatchMaker();
        }
    }

    public void setRoomName(string _name)
    {
        roomName = _name;
    }

    public void CreateRoom()
    {
        if(roomName != "" && roomName != null)
        {
            Debug.Log("Room created");
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", ""  , 0 ,0,  networkManager.OnMatchCreate);
        }
    }
}
