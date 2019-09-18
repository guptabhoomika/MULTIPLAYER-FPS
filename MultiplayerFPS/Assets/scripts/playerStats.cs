using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStats : MonoBehaviour
{
    public Text killCount;


    private void Start()
    {
        if(useraccmanager.isLoggedIn)
        {
            useraccmanager.instance.data_getData_Button( onRecievedData);
        }
        
    }
    void onRecievedData(string data)
    {
        killCount.text = DataTranslator.DataToKills(data).ToString() + " KILLS";
        
    }

}
