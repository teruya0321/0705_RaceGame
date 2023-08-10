using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadMiddleConectPoint : MonoBehaviour
{
    private void Start()
    {
        
    }
    public void ReloadMiddle(GameObject conectTop,GameObject conectUnder)
    {
        conectTop.transform.position = transform.GetChild(0).position;
        conectUnder.transform.position=transform.GetChild(1).position;
    }
}
