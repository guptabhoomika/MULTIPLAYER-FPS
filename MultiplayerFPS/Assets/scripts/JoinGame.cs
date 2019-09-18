using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using UnityEngine.Networking;

public class JoinGame : MonoBehaviour
{
    private NetworkManager networkManager; 

    [SerializeField]
    private Text statusText;

    [SerializeField]
    private GameObject roomListprefab;

    [SerializeField]
    private Transform roomListParent;

   

    List<GameObject> roomList = new List<GameObject>();

    private void Start()
    {
        networkManager = NetworkManager.singleton;
        if(networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
        RefreshRoomList();
    }

    public void RefreshRoomList()
    {
        ClearRoomList();
        networkManager.matchMaker.ListMatches(0, 20, "", false, 0, 0, OnMatchList);
        statusText.text = "Loading...";

    }


    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        statusText.text = "";

        if (roomList == null)
        {
            statusText.text = "No matches found";
            return;
        }

        ClearRoomList();

        foreach (MatchInfoSnapshot match in matches)
        {
            GameObject roomlistGO = (GameObject)Instantiate(roomListprefab);
            roomlistGO.transform.SetParent(roomListParent);
            roomList.Add(roomlistGO);

            roomListitem _roomListItem = roomlistGO.GetComponent<roomListitem>();

            if(_roomListItem != null)
            {
                _roomListItem.Setup(match , JoinRoom);
            }

            if(roomList.Count == 0) 
            {
                 statusText.text = "No rooms found";
            }

        }
    }

    void ClearRoomList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }

        roomList.Clear();
    }

   public void JoinRoom(MatchInfoSnapshot _match)
    {
        Debug.Log("Joining" + _match.name);

        networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        ClearRoomList();
        statusText.text = "Joining...";

    }




}
