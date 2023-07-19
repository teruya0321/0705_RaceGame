using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class SelectInsect : MonoBehaviour
{
    InsectSelectController selectCon;

    // ���ꂼ��̕��ʂ�u����I�u�W�F�N�g�̏ꏊ
    public GameObject[] bodyList;
    // ���ݕ\������Ă���{�f�B�p�[�c

    public List<Transform> conectPoint;

    int bodyNumber = 1;
    // ���ݑI�����Ă��镔��
    int insectNumber = 1;
    // ���ݑI�����Ă��钎�̎��

    float slotTimer;

    bool ready = false;
    void Awake()
    {
        //input = GetComponent<PlayerInput>();
        selectCon = GameObject.FindWithTag("GameController").GetComponent<InsectSelectController>();
    }
    void Start()
    {
        foreach(Transform c in transform) conectPoint.Add(c);

        for (int i = 1; i <= 3; i++)
        {
            bodyList[i - 1] = Instantiate((GameObject)Resources.Load("TestPrefab/" + selectCon.insectSelectCSVDatas[insectNumber][i]), conectPoint[i - 1]);
            //Debug.Log(bodyList[i - 1]);
        } 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebugInsectDecide();
        }
        if (ready)
        {
            if (InsectSelectController.instance.dic[gameObject.name] == null)
            {
                //InsectSelectController.instance.dic[gameObject.name] = gameObject;
                InsectSelectController.instance.InsectList(gameObject);
            }
            else return;
        }
        

        slotTimer += Time.deltaTime;

        if(slotTimer >=  0.2f)
        {
            ChangeBody();
        }

        if (bodyNumber > 3) ready = true;
    }

    void ChangeBody()
    {
        if (ready) return;

        
        if (bodyList[bodyNumber - 1] != null) Destroy(bodyList[bodyNumber - 1]);

        // �O��I�����Ă����I�u�W�F�N�g���폜

        insectNumber++;
        // ���̎�ނ�ύX
        if (insectNumber > 2) insectNumber = 1;
        // ���̎�ނ��I���ł���͈͂𒴂�����A���ꂼ��ŏ��l�A�ő�l�ɂ���
        slotTimer = 0;
        bodyList[bodyNumber - 1] = Instantiate((GameObject)Resources.Load("TestPrefab/" + selectCon.insectSelectCSVDatas[insectNumber][bodyNumber]),
                                   conectPoint[bodyNumber - 1]);
        // CSV����Ή������I�u�W�F�N�g�𐶐�����
    }
    // �R���g���[���[�p�̊֐�
    public void InsectDecide(InputAction.CallbackContext context)
    {
        if (ready) return;
        if (!context.performed) return;

        bodyNumber++;
        insectNumber = 1;
        slotTimer = 0;
    }

    // PC�Ńf�o�b�O�p�̊֐� ����܎g��Ȃ�������������
    void DebugInsectDecide()
    {
        if (ready) return;

        bodyNumber++;
        insectNumber = 1;
        slotTimer = 0;
    }
}
