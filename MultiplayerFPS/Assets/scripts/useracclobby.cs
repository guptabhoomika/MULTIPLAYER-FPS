using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class useracclobby : MonoBehaviour
{
    public Text username;
    private void Start()
    {
        if(useraccmanager.isLoggedIn)
        {
            username.text = useraccmanager.loggedIn_Username;
        }
    }
    public void SignOut()
    {
        if(useraccmanager.isLoggedIn)
        {
            useraccmanager.instance.LogOut();
        }
    }

}
