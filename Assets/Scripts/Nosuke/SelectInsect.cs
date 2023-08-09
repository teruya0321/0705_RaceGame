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
    // ���̃X���b�g�^�C�}�[
    bool ready = false;
    // �����������Ă邩�̔���

    AudioSource audioSource;
    // ���ʉ���炷���߂̃R���|�[�l���g
    public AudioClip selectSE;
    //�I���������̌��ʉ�

    ReloadMiddleConectPoint middleConect;

    void Awake()
    {
        //input = GetComponent<PlayerInput>();
        selectCon = GameObject.FindWithTag("GameController").GetComponent<InsectSelectController>();
        // InsectSelectcontroller���^�O�Ō������ĕϐ��ɂԂ�����

        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        foreach(Transform c in transform) conectPoint.Add(c);
        // �ڑ��p�̃|�C���g��ϐ��ɓ����

        for (int i = 1; i <= 3; i++)
        {
            bodyList[i - 1] = Instantiate((GameObject)Resources.Load("TestPrefab/" + selectCon.insectSelectCSVDatas[insectNumber][i]), conectPoint[i - 1]);
            // �ŏ��̃I�u�W�F�N�g��ݒ�ɓ����
        } 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebugInsectDecide();
            // �f�o�b�O�p�̃L�[����
        }
        if (ready) // ����I�����I�������
        {
            if (InsectSelectController.instance.dic[gameObject.name] == null) // ��񂾂��󂯓n�����߂̐ݒ�
            {
                InsectSelectController.instance.InsectList(gameObject);
                // �I���������̃f�[�^��InsectSelectController�Ɏ󂯓n��
            }
            else return; // �󂯓n�����I�������炻��ȍ~�̏������~�߂�
        }
        

        slotTimer += Time.deltaTime; // ���X���b�g�̃^�C�}�[

        if(slotTimer >=  0.2f)
        {
            ChangeBody();
            // �^�C�}�[�Ŋ֐��𔭓�������
        }

        if (bodyNumber > 3) ready = true; // ���p�[�c��I�����I�����珀�������ɂ���
    }

    void ChangeBody()
    {
        if (ready) return; // �����������Ă���Ȃ珈���������Ȃ�

        
        if (bodyList[bodyNumber - 1] != null) Destroy(bodyList[bodyNumber - 1]);
        // �O��I�����Ă����I�u�W�F�N�g���폜

        insectNumber++;
        // ���̎�ނ�ύX
        if (insectNumber > 4) insectNumber = 1;
        // ���̎�ނ̑I���ł���͈͂𒴂�����A�����l�ɖ߂�
        slotTimer = 0;
        bodyList[bodyNumber - 1] = Instantiate((GameObject)Resources.Load("TestPrefab/" + selectCon.insectSelectCSVDatas[insectNumber][bodyNumber]),
                                   conectPoint[bodyNumber - 1]);
        // CSV����Ή������I�u�W�F�N�g�𐶐�����

        if(bodyNumber == 2)
        {
            middleConect = bodyList[bodyNumber - 1].AddComponent<ReloadMiddleConectPoint>();

            middleConect.ReloadMiddle(conectPoint[0].gameObject, conectPoint[2].gameObject);
        }
    }
    // �R���g���[���[�p�̊֐�
    public void InsectDecide(InputAction.CallbackContext context)
    {
        if (ready) return;
        if (!context.performed) return;

        bodyNumber++;
        // ���̃p�[�c�ɐi�߂�
        insectNumber = 1;
        // ���̃i���o�[������������
        slotTimer = 0;
        // �^�C�}�[�����Z�b�g
        audioSource.PlayOneShot(selectSE);
        // ���ʉ���炷
    }

    // PC�Ńf�o�b�O�p�̊֐� ����܎g��Ȃ�������������
    // ���e�̓R���g���[���[�Ɠ���
    void DebugInsectDecide()
    {
        if (ready) return;

        bodyNumber++;
        insectNumber = 1;
        slotTimer = 0;
        audioSource.PlayOneShot(selectSE);
    }
}
