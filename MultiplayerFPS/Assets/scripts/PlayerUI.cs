using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    RectTransform fuelBg;

    [SerializeField]
    GameObject pauseUi;

    playercontroller controller;
    float fuelAmt;
    


    private void Start()
    {
        pauseMenu.isOn = false;
        
    }

    public void SetPlayerController(playercontroller _controller)
    {
        controller = _controller;
    }
    private void Update()
    {
        fuelAmt = controller.getFuelAmt();
        SetFuelAmt(fuelAmt);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TooglePauseMenu();
        }
    }
    void SetFuelAmt(float _amt)
    {
        fuelBg.localScale = new Vector3(1f, _amt, 1f);
    }

    void TooglePauseMenu()
    {
        pauseUi.SetActive(!pauseUi.activeSelf);
        pauseMenu.isOn = pauseUi.activeSelf;

    }


}
