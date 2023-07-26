using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OverallRankingCon : MonoBehaviour
{
    public void BackTitle(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SceneManager.LoadScene(0);
    }

}
