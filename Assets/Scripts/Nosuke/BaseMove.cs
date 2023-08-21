using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMove : MonoBehaviour
{
    Vector3 firstPos; // �����ʒu

    float moveSpeed = 2; // �y��̓�������
    float amplitude = 0.05f; // �y��̓������̑傫��
    // ����public�ɂ��Ă���������

    float rotation; // ��]�Ŏg���l
    void Start()
    {
        // �����ʒu��ݒ�
        firstPos = transform.position;
    }
    void Update()
    {
        // �ŏ��ɐݒ肵�����������Ɠ������ɑ΂��āA�y����t���t���Ɠ������Ă���
        transform.position = firstPos + new Vector3(0, Mathf.Sin(Time.time * moveSpeed) * amplitude, 0);

        rotation += Time.deltaTime * moveSpeed * 10; // �l���ǂ�ǂ񑫂��Ă���

        // ��]������
        transform.localEulerAngles = new Vector3(0, rotation, 0);
    }
}
