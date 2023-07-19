using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Nos : MonoBehaviour
{
    
    void Start()
    {
        for (int i = 1; i <= InsectSelectController.instance.playerList.Count; i++)
        {
            int x = Random.Range(-10, 10);
            int y = Random.Range(-10, 10);

            Vector3 pos = new Vector3(x, y, 0);

            Debug.Log(InsectSelectController.instance.dic["Player" + i]);
            InsectSelectController.instance.dic["Player" + i].transform.position = pos;


        }
    }
}
