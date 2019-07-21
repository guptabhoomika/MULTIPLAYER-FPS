using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    RectTransform fuelBg;
    playercontroller controller;
    float fuelAmt;
    public void SetPlayerController(playercontroller _controller)
    {
        controller = _controller;
    }
    private void Update()
    {
        fuelAmt = controller.getFuelAmt();
        SetFuelAmt(fuelAmt);
    }
    void SetFuelAmt(float _amt)
    {
        fuelBg.localScale = new Vector3(1f, _amt, 1f);
    }


}
