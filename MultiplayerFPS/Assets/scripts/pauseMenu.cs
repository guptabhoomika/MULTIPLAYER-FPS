using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;



public class pauseMenu : MonoBehaviour
{
    public static bool isOn = false;

    NetworkManager networkmanager;

    private void Start()
    {
        networkmanager = NetworkManager.singleton;
    }
     public void LeaveRoom()
    {
        MatchInfo matchinfo = networkmanager.matchInfo;
        networkmanager.matchMaker.DropConnection(matchinfo.networkId, matchinfo.nodeId, 0, networkmanager.OnDropConnection);
        networkmanager.StopHost();

    }
}
