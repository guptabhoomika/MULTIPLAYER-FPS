using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(playerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(Animator))]

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

    [SerializeField]
    private LayerMask environmentMask;

    [SerializeField]
    private float fuelBurnspeed = 1f;
    [SerializeField]
    private float fuelregainSpeed = 0.3f;
    private float actuaFuelamt = 1f;




    ConfigurableJoint joint;

    private Animator animator;
    
    playerMotor motor;
    public float getFuelAmt()
    {
        return actuaFuelamt;
    }
    private void Start()
    {
        motor = GetComponent<playerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        animator = GetComponent<Animator>();
        setJointSettings(jointSpring);
    }
    private void Update()
    {

        if (pauseMenu.isOn)
            return;
        RaycastHit _Hit;
        if(Physics.Raycast(transform.position , Vector3.down , out _Hit, 100f , environmentMask))
        {
            joint.targetPosition = new Vector3(0f, -_Hit.point.y, 0f);
        }
        else
        {
            joint.targetPosition = new Vector3(0f, 0f, 0f);
        }

        float _xmov = Input.GetAxis("Horizontal");
        float _zmov = Input.GetAxis("Vertical");
        Vector3 _moveHorizontal = transform.right * _xmov;
        Vector3 _moveVertical = transform.forward * _zmov;
        Vector3 velocity = (_moveHorizontal + _moveVertical) * speed;
        animator.SetFloat("Blend", _zmov);
        motor.move(velocity);
        float _yrot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yrot, 0f) * lookSensitivity;
        motor.getRotation(_rotation);
        float _xrot = Input.GetAxisRaw("Mouse Y");
        float _camerarotationX = _xrot * lookSensitivity;
        motor.getCameraRotation(_camerarotationX);
        Vector3 thrusterForce = Vector3.zero;
        if(Input.GetButton("Jump") &&  actuaFuelamt >0f)
        {
            actuaFuelamt -= fuelBurnspeed * Time.deltaTime;
            if(actuaFuelamt >= 0.01f)
            {
                thrusterForce = Vector3.up * thurstSpeed;
                setJointSettings(0f);

            }
            
        }
        else
        {
            actuaFuelamt += fuelregainSpeed * Time.deltaTime;
            setJointSettings(jointSpring);
        }

        actuaFuelamt = Mathf.Clamp(actuaFuelamt, 0f, 1f);

        motor.ApplyThrust(thrusterForce);


    }
    private  void setJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive { mode = jointDrive, positionSpring = _jointSpring, maximumForce = jointMaxforce };
    }

}
