using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    List<GameObject> cameraList = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        foreach(Transform cameras in transform)
        {
            cameraList.Add(cameras.gameObject);
        }
    }

    private void Start()
    {
        for(int i = 1; i <= 4;  i++)
        {
            cameraList[i - 1].transform.SetParent(InsectSelectController.instance.dic["Player" + i].transform);

            cameraList[i - 1].transform.position = InsectSelectController.instance.dic["Player" + i].transform.position;
        }
    }
}
