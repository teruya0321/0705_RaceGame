using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCreate : MonoBehaviour
{
    public GameObject startEffect;
    public GameObject endEffect;
    public void StartEffect()
    {
        Instantiate(startEffect,transform);
    }

    public void EndEffect()
    {
        Instantiate (endEffect,transform);
    }
}
