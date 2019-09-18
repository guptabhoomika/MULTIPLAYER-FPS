using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseControl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class useraccmanager : MonoBehaviour
{

    public static useraccmanager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(instance);

    }

    //These store the username and password of the player when they have logged in
    public static string loggedIn_Username { get; protected set; }  //stores username once logged in
    private static string loggedIn_Password = ""; //stores password once logged in
    public static  string loggedin_data { get; protected set; }
    public static bool isLoggedIn { get; protected set; }
    public string LoggedInScene = "Lobby";
    public string LoggedOutScene = "LoginMenu";
    public delegate void OnRecievedDataCallBack(string data);
    public void LogOut()
    {
        loggedIn_Username = "";
        loggedIn_Password = "";

        isLoggedIn = false;
        SceneManager.LoadScene(LoggedOutScene);
       
    }

    public void LogIn(string username , string password)
    {
        loggedIn_Username = username;
        loggedIn_Password = password;

        isLoggedIn = true;
        SceneManager.LoadScene(LoggedInScene);
    }
    public void SendData(string data)
    { //called when the 'Send Data' button on the data part is pressed
        if (isLoggedIn)
        {
            //ready to send request
            StartCoroutine(sendSendDataRequest(loggedIn_Username, loggedIn_Password, data)); //calls function to send: send data request
            
        }
    }

    IEnumerator sendSendDataRequest(string username, string password, string data)
    {

       
        

        IEnumerator eee = DatabaseControl.DCF.SetUserData(username, password, data);
        while (eee.MoveNext())
        {
            yield return eee.Current;
        }
        WWW returneddd = eee.Current as WWW;
        if (returneddd.text == "ContainsUnsupportedSymbol")
        {
            //One of the parameters contained a - symbol
            Debug.Log("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
        }
        if (returneddd.text == "Error")
        {
            //Error occurred. For more information of the error, DatabaseControl.DCF.Login could
            //be used with the same username and password
            Debug.Log("Data Upload Error: Contains Unsupported Symbol '-'");
        }
        
    
        
    }

    public void data_getData_Button(OnRecievedDataCallBack onRecievedData)
    { //called when the 'Get Data' button on the data part is pressed

        if (isLoggedIn)
        {
            //ready to send request
            StartCoroutine(sendGetDataRequest(loggedIn_Username, loggedIn_Password, onRecievedData)); //calls function to send get data request
           
        }
    }

    IEnumerator sendGetDataRequest(string username, string password , OnRecievedDataCallBack onRecievedData)
    {

    string data = "ERROR";

        IEnumerator eeee = DatabaseControl.DCF.GetUserData(username, password);
        while (eeee.MoveNext())
        {
            yield return eeee.Current;
        }
        string returnedd = eeee.Current as string;
        if (returnedd== "Error")
        {
            //Error occurred. For more information of the error, DatabaseControl.DCF.Login could
            //be used with the same username and password
                
            Debug.Log("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
        }
        else
        {
            if (returnedd == "ContainsUnsupportedSymbol")
            {
                //One of the parameters contained a - symbol
                    
                Debug.Log("Get Data Error: Contains Unsupported Symbol '-'");
            }
            else
            {
                //Data received in returned.text variable
                string DataRecieved = returnedd;
                data = DataRecieved;
                    
            }
            if(onRecievedData!=null)
            {
                onRecievedData.Invoke(data);
            }
            
        }
    
        
    }






}

