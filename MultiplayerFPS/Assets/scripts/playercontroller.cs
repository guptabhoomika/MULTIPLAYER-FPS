using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(playerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]

public class playercontroller : MonoBehaviour
{
    [SerializeField]
    private float speed  = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    private float thurstSpeed = 1000f;
    [Header("Joint Settings")]
    [SerializeField]
    private JointDriveMode jointDrive = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxforce= 40f;


    ConfigurableJoint joint;
    
    playerMotor motor;
    private void Start()
    {
        motor = GetComponent<playerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        setJointSettings(jointSpring);
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
        float _camerarotationX = _xrot * lookSensitivity;
        motor.getCameraRotation(_camerarotationX);
        Vector3 thrusterForce = Vector3.zero;
        if(Input.GetButton("Jump"))
        {
            thrusterForce = Vector3.up * thurstSpeed;
            setJointSettings(0f);
        }
        else
        {
            setJointSettings(jointSpring);
        }
        motor.ApplyThrust(thrusterForce);


    }
    private  void setJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive { mode = jointDrive, positionSpring = _jointSpring, maximumForce = jointMaxforce };
    }

}
