using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Nos : MonoBehaviour
{
    public float maxPower;
    public float angle;
    public float breake;
    public WheelCollider wcFL,wcFR,wcBL,wcBR;
    enum Drive {FRONT,BACK}

    Drive drive;
    private void Start()
    {
        drive = Drive.FRONT;
    }
    void DRIVE()
    {
        float power = maxPower * Input.GetAxis("Vertical");
        float steering = angle * Input.GetAxis("Horizontal");

        wcFL.steerAngle = steering;
        wcFR.steerAngle = steering;

        if(drive == Drive.BACK)
        {
            wcBR.motorTorque = power;
            wcBL.motorTorque = power;
        }
        else
        {
            wcFR.motorTorque = power;
            wcFL.motorTorque = power;
        }

    }

    void BREAKE()
    {
        if (Input.GetKey(KeyCode.LeftShift))
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
}
