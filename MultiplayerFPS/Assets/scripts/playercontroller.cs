using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(playerMotor))]

public class playercontroller : MonoBehaviour
{
    [SerializeField]
    private float speed  = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    
    playerMotor motor;
    private void Start()
    {
        motor = GetComponent<playerMotor>();
    }
    private void Update()
    {
        float _xmov = Input.GetAxisRaw("Horizontal");
        float _zmov = Input.GetAxisRaw("Vertical");
        Vector3 _moveHorizontal = transform.right * _xmov;
        Vector3 _moveVertical = transform.forward * _zmov;
        Vector3 velocity = (_moveHorizontal + _moveVertical).normalized * speed;
        motor.move(velocity);
        float _yrot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yrot, 0f) * lookSensitivity;
        motor.getRotation(_rotation);
        float _xrot = Input.GetAxisRaw("Mouse Y");
        Vector3 _camerarotation = new Vector3(_xrot, 0f, 0f) * lookSensitivity;
        motor.getCameraRotation(_camerarotation);

    }

}
