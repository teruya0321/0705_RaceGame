using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepValue : MonoBehaviour
{
    public static KeepValue keepValue;

    public Dictionary<int,GameObject> playInsectObj = new Dictionary<int,GameObject>();

    void Awake()
    {
        if(keepValue == null)
        {
            keepValue = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
