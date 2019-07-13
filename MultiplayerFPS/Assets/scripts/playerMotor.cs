using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class playerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float camerarotationX;
    private float currentCamerarotation = 0f;
    private Vector3 thrustForce = Vector3.zero;
    [SerializeField]
    private float cameraRotationLimit = 85f;


    private Rigidbody rb;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void getRotation(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    public void getCameraRotation(float _camerarotationX)
    {
        camerarotationX = _camerarotationX;
    }

    public void ApplyThrust(Vector3 _thrustForce)
    {
        thrustForce = _thrustForce;
    }


    private void FixedUpdate()
    {
        performMovement();
        performRotation();
    }
    void performMovement()
    {
        if(velocity!= Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        if(thrustForce != Vector3.zero)
        {
            rb.AddForce(thrustForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
    void performRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam!= null)
        {
            currentCamerarotation -= camerarotationX;
            currentCamerarotation = Mathf.Clamp(currentCamerarotation, -cameraRotationLimit, +cameraRotationLimit);
            cam.transform.localEulerAngles = new Vector3(currentCamerarotation, 0f, 0f);
            
        }
    }
}
