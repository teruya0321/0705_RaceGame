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
        input = GetComponent<PlayerInput>();
        input.defaultActionMap = gameObject.name;
    }

    private void Update()
    {
        if (input.currentActionMap.FindAction("MoveHorizontal").ReadValue<float>() >= 0.8f || input.currentActionMap.FindAction("MoveHorizontal").ReadValue<float>() <= -0.8f)
            Debug.Log(gameObject.name + ":横:" + input.currentActionMap.FindAction("MoveHorizontal").ReadValue<float>());
        if (input.currentActionMap.FindAction("MoveVertical").ReadValue<float>() >= 0.8f || input.currentActionMap.FindAction("MoveVertical").ReadValue<float>() <= -0.8f) 
            Debug.Log(gameObject.name + ":縦:" + input.currentActionMap.FindAction("MoveVertical").ReadValue<float>());

        //if (input.currentActionMap.FindAction("MoveHorizontal").IsPressed()) 

        // 横のスティックの値をDebug.Logで取得。デバッグログが長く出るのがうざかったから値の制限をつけてるけど、
        // 小さい値からとりたいときは.ReadValue<float>() >= 0.8fという部分を.isPressed()に変更してください。
    }


    // Event型で取得するためpublicに。PlayerInputコンポーネントと合わせて使う。わからんかったら聞いてくれ
    public void JumpButton(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log(gameObject.name + "がジャンプを押したぜ！");
    }
    public void AttackButton(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log(gameObject.name + "がアタックしたぜ！");
    }

    // 下二つは長押しの判定をつけたいから要改良
    public void AccelButton(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log(gameObject.name + "がアクセルをふかすぜ！");
    }
    public void BrakeButton(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log(gameObject.name + "がブレーキをかけるぜ！");
    }
}
