using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WheelColliderTest : MonoBehaviour
{
    public float maxPower;

    public float angle;
    public float breake;
    public WheelCollider wcFL, wcFR, wcBL, wcBR;
    public PlayerInput input;

    public GameObject player;

    public Rigidbody middleRb;

    public bool walking = false;

    public bool goal = false;

    public GameObject top;
    public GameObject bottom;
    private void OnEnable()
    {
        //player = transform.parent.parent.gameObject;

        middleRb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        input = GetComponent<PlayerInput>();
    }
    public void DRIVE()
    {
        float power = maxPower * input.currentActionMap.FindAction("Accel").ReadValue<float>();
        float steering = angle * input.currentActionMap.FindAction("MoveHorizontal").ReadValue<float>();

        wcFL.steerAngle = steering;
        wcFR.steerAngle = steering;
        wcBL.steerAngle = steering * -1;
        wcBR.steerAngle = steering * -1;

        wcFR.motorTorque = power;
        wcFL.motorTorque = power;
        wcBR.motorTorque = power;
        wcBL.motorTorque = power;
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
        if (input.currentActionMap.FindAction("Accel").ReadValue<float>() != 0)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }

        top.transform.position = transform.GetChild(0).position;
        top.transform.rotation = transform.rotation;

        bottom.transform.position = transform.GetChild(1).position;
        bottom.transform.rotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
