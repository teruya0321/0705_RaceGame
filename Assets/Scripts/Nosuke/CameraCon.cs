using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    public Vector3 cameraPos;
    public float cameraLotationX;
    Vector3 cameraLot;
    // Start is called before the first frame update
    void Start()
    {
        cameraLot.x = cameraLotationX;

        GameObject parent = GameObject.FindWithTag(gameObject.tag);
        transform.position = cameraPos + parent.transform.position;

        transform.SetParent(parent.transform);

        transform.localEulerAngles = cameraLot + Vector3.zero;
    }

    void CameraPosReset()
    {
        
    }
}
