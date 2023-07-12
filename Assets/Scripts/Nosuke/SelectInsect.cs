using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInsect : MonoBehaviour
{
    public Transform[] bodyparts;
    // ���ꂼ��̕��ʂ�u����I�u�W�F�N�g�̏ꏊ
    GameObject[] bodyList;
    // ���ݕ\������Ă���{�f�B�p�[�c

    int bodyNumber;
    // ���ݑI�����Ă��镔��
    int insectNumber;
    // ���ݑI�����Ă��钎�̎��

    void Update()
    {
        if(Input.GetAxisRaw("Vertical") > 0) ChangeBody(1);
        else if(Input.GetAxisRaw("Vertical") < 0) ChangeBody(-1);
        // �p�[�c�̎�ނ�ς���

        if (Input.GetAxisRaw("Horizontal") > 0) bodyNumber++;
        else if (Input.GetAxisRaw("Horizontal") < 0) bodyNumber--;
        // �I�����Ă��镔�ʂ�ς���

        if(bodyNumber > bodyparts.Length) bodyNumber = 0;
        else if(bodyNumber < 0) bodyNumber = bodyparts.Length;
        // �p�[�c���I���ł���͈͂𒴂�����A���ꂼ��ŏ��l�A�ő�l�ɂ���
    }

    void ChangeBody(int i)
    {
        Destroy(bodyList[bodyNumber]);
        // �O��I�����Ă����I�u�W�F�N�g���폜

        insectNumber += i;
        // ���̎�ނ�ύX
        if (insectNumber > 9) insectNumber = 1;
        else if (insectNumber < 0) insectNumber = 9;
        // ���̎�ނ��I���ł���͈͂𒴂�����A���ꂼ��ŏ��l�A�ő�l�ɂ���

        bodyList[bodyNumber] = Instantiate((GameObject)Resources.Load("LoadPrefabs/"), bodyparts[bodyNumber].position, Quaternion.identity, transform);
        // CSV����Ή������I�u�W�F�N�g�𐶐�����
    }
}
