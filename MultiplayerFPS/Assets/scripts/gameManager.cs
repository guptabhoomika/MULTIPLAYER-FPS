using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public static gameManager instance;
    public MatchSettings matchsettings;

    [SerializeField]
    private GameObject sceneCam;

    private void Awake()
    {
        if(instance!=null)
        {

            Debug.LogError("More than one gamemanger");
        }
        else
        {
            instance = this;
        }
    }
    #region playerTracking
    private const string PLAYER_PREFIX =  "Player ";
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string _id , Player _player)
    {
        string _playerID = PLAYER_PREFIX + _id;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }
    public static void DergisterPlayer(string _ID)
    {
        players.Remove(_ID);
    }
   public static Player getPlayer(string _playerID)
    {
        return players[_playerID];
    }
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200 ,200 , 200, 500));
        GUILayout.BeginVertical();

        foreach (string _playerID in players.Keys)
        {
            GUILayout.Label(_playerID + " " + players[_playerID].transform.name);
        }


        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    #endregion

    public void SetCameraActive(bool isactive)
    {
        if (sceneCam == null)
            return;
        sceneCam.SetActive(isactive);

    }
}
