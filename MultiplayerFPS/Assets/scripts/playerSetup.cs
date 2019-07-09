using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componenetsToDisable;
    Camera sceneCamera;
    private void Start()
    {
        if(!isLocalPlayer)
        {
            for (int i = 0; i < componenetsToDisable.Length; i++)

            {
                componenetsToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);

            }
               
        }
    }
    private void OnDisable()
    {
        if(sceneCamera!= null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
