using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InsectMove : MonoBehaviour
{
    public Transform cameraParent;

   // bool ready = true;

    public SelectInsect selectInsect;

    Animator[] animators = new Animator[3];
    ParticleSystem particle;

    public Test_Nos wheelscript;

    PlayerInput player;

    public bool goal = false;

    private void OnEnable()
    {
        selectInsect = GetComponent<SelectInsect>();

        player = GetComponent<PlayerInput>();

        cameraParent = transform.GetChild(3);

        particle = GetComponent<ParticleSystem>();
    }
    void Start()
    {
        for(int i = 1; i <= 3; i++)
        {
            selectInsect.bodyList[i - 1].gameObject.tag = "Player";
            if(i == 2)
            {
                animators[i - 1] = selectInsect.bodyList[i - 1].gameObject.GetComponent<Animator>();
            }
            else
            {
                animators[i - 1] = selectInsect.bodyList[i - 1].transform.GetChild(0).gameObject.GetComponent<Animator>();
            }
        }
        wheelscript = selectInsect.bodyList[1].GetComponent<Test_Nos>();

        wheelscript.enabled = true;

        cameraParent.SetParent(null);

        transform.SetParent(null);
    }

    private void Update()
    {
        if (goal) return;

        for(int i = 0; i < 3; i++)
        {
            animators[0].SetBool("Walk", wheelscript.walking);
            animators[1].SetBool("Walk", wheelscript.walking);
            animators[2].SetBool("Walk", wheelscript.walking);
        }

        if(player.currentActionMap.FindAction("Accel").ReadValue<float>() != 0)
        {
            particle.Play();
        }
        else
        {
            particle.Stop();
        }
    }

    private void LateUpdate()
    {
        

        if (goal) return;
        cameraParent.transform.position = transform.position;

        transform.localEulerAngles += new Vector3(0, player.currentActionMap.FindAction("MoveHorizontal").ReadValue<float>() * 10, 0);

        Vector3 cameraLot = new Vector3(0, player.currentActionMap.FindAction("Lotation").ReadValue<float>(), 0);

        cameraParent.transform.localEulerAngles += cameraLot;

        if (player.currentActionMap.FindAction("LotationReset").IsPressed())
        {
            RotationReset();
        }

        if(player.currentActionMap.FindAction("CameraReset").IsPressed())
        {
            CameraResetButton();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

        if(collision.gameObject.tag == "UnderGround")
        {
            transform.localEulerAngles = Vector3.zero;
            transform.position = Vector3.up * 100f;
        }
    }

    void CameraResetButton()
    {
        cameraParent.transform.localEulerAngles = Vector3.zero;
    }

    void RotationReset()
    {
        transform.localEulerAngles = Vector3.zero;
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        //transform.position += Vector3.forward;
    }
}
