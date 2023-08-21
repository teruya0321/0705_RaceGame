using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InsectMove : MonoBehaviour
{
    PlayerInput input;
    //InputAction action;

    Rigidbody myRb;
    public float speed = 500f;

    bool isGround = false;
    public Transform cameraParent;

    bool ready = true;
    void Start()
    {
        input = GetComponent<PlayerInput>();
        myRb =  gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!ready) return;

        Vector3 rbAddTorque = new Vector3(0, input.currentActionMap.FindAction("MoveHorizontal").ReadValue<Vector2>().x * speed * 10, 0);

        myRb.AddTorque(rbAddTorque);

        Vector3 playerLot = transform.localEulerAngles;

        float LotX = playerLot.x;
        if (LotX > 180) LotX -= 360;
        float x = Mathf.Clamp(LotX, -30, 45);

        float LotZ = playerLot.z;
        if(LotZ > 180) LotZ -= 360;
        float z = Mathf.Clamp(LotZ, -30, 30);

        playerLot.x = x;
        playerLot.z = z;
        transform.localEulerAngles = playerLot;

       //Mathf.Clamp(transform.localEulerAngles.x, -45, 45);
       // Mathf.Clamp(transform.localEulerAngles.z, -45, 45);

        cameraParent.transform.position = transform.position;

        //--------------------------------------------------------------------------------------------------------------------

        Vector3 cameraLot = new Vector3(input.currentActionMap.FindAction("Lotation").ReadValue<Vector2>().y * -1, input.currentActionMap.FindAction("Lotation").ReadValue<Vector2>().x, 0);

        cameraParent.localEulerAngles += cameraLot;
        if (isGround)
        {
            myRb.AddForce(input.currentActionMap.FindAction("Accel").ReadValue<float>() *  speed * transform.forward);

            myRb.AddForce(input.currentActionMap.FindAction("Brake").ReadValue<float>() * -1 * speed * transform.forward);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }

        if(collision.gameObject.tag == "Goal")
        {
            ready = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    public void JumpButton(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!isGround) return;

        myRb.AddForce(transform.up * 30000);  
    }

    public void CameraResetButton(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        cameraParent.transform.localEulerAngles = transform.localEulerAngles;
    }
}
