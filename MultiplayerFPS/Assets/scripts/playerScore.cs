using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Player))]
public class playerScore : MonoBehaviour
{

    Player player;
   
    void Start()
    {
        player = GetComponent<Player>();

        StartCoroutine(SyncDataLoop());
        
    }


    private void OnDestroy()
    {
        if(player !=null)
        {
            SyncNow();
        }
    }

    IEnumerator SyncDataLoop()
    {

        while (true)
        {
            yield return new WaitForSeconds(5f);

            SyncNow();


        }



    }

    void SyncNow()
    {
        if (useraccmanager.isLoggedIn)
        {
            useraccmanager.instance.data_getData_Button(OnRecievedData);
        }
    }

    void OnRecievedData(string data)
    {
        if (player.kills == 0)
            return;

        int kills = DataTranslator.DataToKills(data);

        int newKills = player.kills + kills;

        string newData = DataTranslator.ValuesToData(kills);

        Debug.Log("Syncing");

        player.kills = 0;


        useraccmanager.instance.SendData(newData);

       

    }
    
}
