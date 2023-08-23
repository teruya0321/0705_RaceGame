using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Nos : MonoBehaviour
{
    public float maxPower;
    
    public float angle;
    public float breake;
    public WheelCollider wcFL,wcFR,wcBL,wcBR;
    public PlayerInput input;

    public GameObject player;

    public Rigidbody middleRb;

    public bool walking = false;

    public bool goal = false;
    private void OnEnable()
    {
        player = transform.parent.parent.gameObject;

        middleRb = GetComponent<Rigidbody>();

        gameObject.AddComponent<FixedJoint>().connectedBody = player.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        input = player.GetComponent<PlayerInput>();

        
    }
    public void DRIVE()
    {
        float power = maxPower * input.currentActionMap.FindAction("Accel").ReadValue<float>();
        float back = maxPower * input.currentActionMap.FindAction("Back").ReadValue<float>() * -1;
        float steering = angle * input.currentActionMap.FindAction("MoveHorizontal").ReadValue<float>();

        wcFL.steerAngle = steering;
        wcFR.steerAngle = steering;
        //wcBL.steerAngle = steering * -1;
        //wcBR.steerAngle = steering * -1;

        wcFR.motorTorque = power + back;
        wcFL.motorTorque = power + back;
        wcBR.motorTorque = power + back;
        wcBL.motorTorque = power + back;
    }

    public void BREAKE()
    {
        if (input.currentActionMap.FindAction("Brake").IsPressed())
        {
            wcFL.brakeTorque = breake;
            wcFR.brakeTorque = breake;
            wcBL.brakeTorque = breake;
            wcBR.brakeTorque = breake;
        }
        else
        {
            wcFL.brakeTorque = 0;
            wcFR.brakeTorque = 0;
            wcBL.brakeTorque = 0;
            wcBR.brakeTorque = 0;
        }
    }

    private void FixedUpdate()
    {
        if (goal) return;

        DRIVE();
        BREAKE();
    }

    private void Update()
    {
        if (goal) return;

        if(input.currentActionMap.FindAction("Accel").ReadValue<float>() != 0)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
