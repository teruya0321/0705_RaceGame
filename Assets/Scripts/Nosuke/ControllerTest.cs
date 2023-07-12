using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class ControllerTest : MonoBehaviour
{
    [SerializeField] PlayerInput input; 
    InputAction action;

    private void Awake()
    {

    }

    private void Update()
    {
        if (input.currentActionMap.FindAction("MoveHorizontal").IsPressed()) Debug.Log("�����您����������������������");
        if (input.currentActionMap.FindAction("MoveVertical").IsPressed()) Debug.Log("�c���您������������������������");
    }
}
