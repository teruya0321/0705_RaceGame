using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class ControllerTest : MonoBehaviour
{

    [SerializeField] PlayerInput input; 
    InputAction action;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        //sinput.defaultActionMap = gameObject.name;
    }

    private void Update()
    {
        if(input.currentActionMap.FindAction("MoveHorizontal").ReadValue<Vector2>() != Vector2.zero)
        {
            Debug.Log(input.currentActionMap.FindAction("MoveHorizontal").ReadValue<Vector2>());
        }

        if(input.currentActionMap.FindAction("Lotation").ReadValue<float>() != 0)
        {
            Debug.Log(input.currentActionMap.FindAction("Lotation").ReadValue<float>());
        }
    }

    /*private void Update()
    {
        if (input.currentActionMap.FindAction("MoveHorizontal").ReadValue<float>() >= 0.8f || input.currentActionMap.FindAction("MoveHorizontal").ReadValue<float>() <= -0.8f)
            Debug.Log(gameObject.name + ":��:" + input.currentActionMap.FindAction("MoveHorizontal").ReadValue<float>());
        if (input.currentActionMap.FindAction("MoveVertical").ReadValue<float>() >= 0.8f || input.currentActionMap.FindAction("MoveVertical").ReadValue<float>() <= -0.8f) 
            Debug.Log(gameObject.name + ":�c:" + input.currentActionMap.FindAction("MoveVertical").ReadValue<float>());

        //if (input.currentActionMap.FindAction("MoveHorizontal").IsPressed()) 

        // ���̃X�e�B�b�N�̒l��Debug.Log�Ŏ擾�B�f�o�b�O���O�������o��̂���������������l�̐��������Ă邯�ǁA
        // �������l����Ƃ肽���Ƃ���.ReadValue<float>() >= 0.8f�Ƃ���������.isPressed()�ɕύX���Ă��������B
    }

    
    // Event�^�Ŏ擾���邽��public�ɁBPlayerInput�R���|�[�l���g�ƍ��킹�Ďg���B�킩��񂩂����畷���Ă���
    public void Horizontal(InputAction.CallbackContext context)
    {
        Vector2 vector2 = context.ReadValue<Vector2>();
        Debug.Log(gameObject.name + "�͉������ɃX�e�B�b�N��" + vector2 + "���炢�|���Ă邺�I");
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log(gameObject.name + "���W�����v�����������I");
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log(gameObject.name + "���A�^�b�N�������I");
    }

    // ����͒������̔��������������v����
    public void Accel(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log(gameObject.name + "���A�N�Z�����ӂ������I");
    }
    public void Brake(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log(gameObject.name + "���u���[�L�������邺�I");
    }*/
}
