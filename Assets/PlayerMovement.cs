using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rB;
    private Transform Camtransform;
    public Camera cam;
    Vector3 input;


    [Header("Character Setup")]
    public float speed;
    public float runSpeed;
    [Header("Camera Setup")]
    public float MaxY = 60f;
    public float MinY = -60f;
    public float SensitivityY = 0.0f;
    public float SensitivityX = 0.0f;
    private float rotationY = 0;
    private float rotationX = 0;

    void Start()
    {
        Camtransform = Camera.main.transform;
        rB = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
       input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // getting the input vector
        rotationY += Input.GetAxis("Mouse Y") * SensitivityY;
        rotationX += Input.GetAxis("Mouse X") * SensitivityX; 
        rotationY = Mathf.Clamp(rotationY, MinY, MaxY); // clamping rotation y to min and max whihc can be set in insepector 
        if (rotationY != 0 || rotationX != 0)
        {
            
            cam.transform.localEulerAngles = new Vector3(-rotationY, 0, 0); // rotating the camera on the y
            this.transform.eulerAngles = new Vector3(0.0f, rotationX, 0.0f);
        }

    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        float LookDir = (Input.GetAxis("Mouse X"));
        Vector3 forward = transform.forward; //transform.forward;
        Vector3 right = transform.right; //transform.right;

        //right.y = 0;

        //forward.Normalize();
        //right.Normalize();

        Vector3 DesMoveDir = ((forward * input.z) + (right * input.x));//.normalized; // creating a vector that takes the input directions and multiplies them with the forward and side of camera so the player moves where it is looking 

        Vector3 DesLookDir = (right * LookDir).normalized; // normalising the look directon

        rB.velocity = DesMoveDir.normalized * speed + new Vector3(0.0f, rB.velocity.y, 0.0f); // setting the velocity of the player so he moves
    }
}
