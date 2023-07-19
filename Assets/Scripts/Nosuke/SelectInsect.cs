using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class SelectInsect : MonoBehaviour
{
    InsectSelectController selectCon;

    // それぞれの部位を置く空オブジェクトの場所
    public GameObject[] bodyList;
    // 現在表示されているボディパーツ

    public List<Transform> conectPoint;

    int bodyNumber = 1;
    // 現在選択している部位
    int insectNumber = 1;
    // 現在選択している虫の種類

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

        // 前回選択していたオブジェクトを削除

        insectNumber++;
        // 虫の種類を変更
        if (insectNumber > 2) insectNumber = 1;
        // 虫の種類が選択できる範囲を超えたら、それぞれ最小値、最大値にする
        slotTimer = 0;
        bodyList[bodyNumber - 1] = Instantiate((GameObject)Resources.Load("TestPrefab/" + selectCon.insectSelectCSVDatas[insectNumber][bodyNumber]),
                                   conectPoint[bodyNumber - 1]);
        // CSVから対応したオブジェクトを生成する
    }
    // コントローラー用の関数
    public void InsectDecide(InputAction.CallbackContext context)
    {
        if (ready) return;
        if (!context.performed) return;

        bodyNumber++;
        insectNumber = 1;
        slotTimer = 0;
    }

    // PCでデバッグ用の関数 あんま使わない方がいいかも
    void DebugInsectDecide()
    {
        if (ready) return;

        bodyNumber++;
        insectNumber = 1;
        slotTimer = 0;
    }
}
