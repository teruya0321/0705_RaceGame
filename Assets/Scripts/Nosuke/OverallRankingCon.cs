using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverallRankingCon : MonoBehaviour
{
    public Transform[] points;

    public Text[] rankInsectNames;
    private void Start()
    {
        for(int i = 1; i <= 3;  i++)
        {
            KeepValue.keepValue.playInsectObj[i].transform.localScale = new Vector3(5, 5, 5);

            KeepValue.keepValue.playInsectObj[i].transform.localEulerAngles = new Vector3(0, 180, 0);

            KeepValue.keepValue.playInsectObj[i].transform.position = points[i - 1].position;

            SelectInsect s = KeepValue.keepValue.playInsectObj[i].GetComponent<SelectInsect>();

            rankInsectNames[i - 1].text = s.bodyName[0] + s.bodyName[1] + s.bodyName[2];
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}
