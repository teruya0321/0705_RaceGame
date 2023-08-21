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
    PlayerInput input;

    bool ready;
    private void Start()
    {
        input = GetComponent<PlayerInput>();
    }
    void DRIVE()
    {
        float power = maxPower * input.currentActionMap.FindAction("Accel").ReadValue<float>();
        float steering = angle * input.currentActionMap.FindAction("MoveHorizontal").ReadValue<Vector2>().x;

        wcFL.steerAngle = steering;
        wcFR.steerAngle = steering;
        wcBL.steerAngle = steering * -1;
        wcBR.steerAngle = steering * -1;

        wcFR.motorTorque = power;
        wcFL.motorTorque = power;
        wcBR.motorTorque = power;
        wcBL.motorTorque = power;
    }

    void BREAKE()
    {
        if (input.currentActionMap.FindAction("Breake").IsPressed())
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
        DRIVE();
        BREAKE();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            ready = true;

            collision.gameObject.GetComponent<GoalScript>().Goal(gameObject);
        }
    }
}
