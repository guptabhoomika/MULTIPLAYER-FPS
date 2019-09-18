using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;


public class roomListitem : MonoBehaviour
{
    private MatchInfoSnapshot match;

    public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
    private JoinRoomDelegate joinRoomCallBack;

    [SerializeField]
    private Text roomName;

     public void Setup(MatchInfoSnapshot _match , JoinRoomDelegate _joinRoomCallBack)
     {
        match = _match;

        joinRoomCallBack = _joinRoomCallBack;

        roomName.text = match.name + "(" + match.currentSize + "/" + match.maxSize + ")" + " " + "<i>click to join</i>";
      }
    public void JoinRoom()
    {
        joinRoomCallBack.Invoke(match);
    }

}
