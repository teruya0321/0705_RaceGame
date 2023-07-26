using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public void ActionButton(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SceneManager.LoadScene(1);
    }
}
