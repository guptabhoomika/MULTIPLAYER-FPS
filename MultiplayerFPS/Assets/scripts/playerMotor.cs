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
    private Vector3 camerarotation = Vector3.zero;


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
    public void getCameraRotation(Vector3 _camerarotation)
    {
        camerarotation = _camerarotation;
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
    }
    void performRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam!= null)
        {
            cam.transform.Rotate(-camerarotation);
        }
    }
}
