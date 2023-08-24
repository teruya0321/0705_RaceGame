using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System.Threading;
using UnityEngine.UI;

public class SelectInsect : MonoBehaviour
{
    PlayerInput input;

    InsectSelectController selectCon;

    // それぞれの部位を置く空オブジェクトの場所
    public GameObject[] bodyList;
    // 現在表示されているボディパーツ

    public List<Transform> conectPoint;

    public int bodyNumber = 1;
    // 現在選択している部位
    int insectNumber = 1;
    // 現在選択している虫の種類

    float slotTimer;
    // 虫のスロットタイマー
    bool ready = false;
    // 準備完了してるかの判定

    AudioSource audioSource;
    // 効果音を鳴らすためのコンポーネント
    public AudioClip selectSE;
    //選択した時の効果音

    ReloadMiddleConectPoint middleConect;

    float cooltimer;

    public bool cooltime;

    TextAsset insectNameCSV;
    List<string[]> insectNameList = new List<string[]>();

    public Text insectName;

    public string[] bodyName;

    public GameObject selectEff;
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        selectCon = GameObject.FindWithTag("GameController").GetComponent<InsectSelectController>();
        // InsectSelectcontrollerをタグで検索して変数にぶち込む

        insectNameCSV = Resources.Load("CSVs/InsectName") as TextAsset;
        StringReader reader = new StringReader(insectNameCSV.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            insectNameList.Add(line.Split(',')); // , 区切りでリストに追加
        }

        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        foreach(Transform c in transform) conectPoint.Add(c);
        // 接続用のポイントを変数に入れる

        for (int i = 1; i <= 3; i++)
        {

            bodyList[i - 1] = Instantiate((GameObject)Resources.Load("LoadPrefabs/" + selectCon.insectSelectCSVDatas[insectNumber][i]),
                                   conectPoint[i - 1]);
            bodyName[i - 1] = insectNameList[1][i];

            if (i == 2)
            {
                middleConect = bodyList[i - 1].AddComponent<ReloadMiddleConectPoint>();
                middleConect.ReloadMiddle(conectPoint[0].gameObject, conectPoint[2].gameObject);
            }
            // 最初のオブジェクトを設定に入れる
        }

        insectName.text = bodyName[0] + bodyName[1] + bodyName[2];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebugInsectDecide();
            // デバッグ用のキー入力
        }

        if (cooltime)
        {
            cooltimer += Time.deltaTime;

            if (cooltimer >= 0.5f)
            {
                cooltime = false;
                cooltimer = 0;
            }
        }

        if (ready) // 虫を選択し終わったら
        {
            if (InsectSelectController.instance.dic[gameObject.name] == null) // 一回だけ受け渡すための設定
            {
                InsectSelectController.instance.InsectList(gameObject);
                // 選択した虫のデータをInsectSelectControllerに受け渡す
            }
            else return; // 受け渡しが終了したらそれ以降の処理を止める
        }
        

        slotTimer += Time.deltaTime; // 虫スロットのタイマー

        if(slotTimer >=  0.2f)
        {
            ChangeBody();
            // タイマーで関数を発動させる
        }

        if (bodyNumber > 3) ready = true; // 虫パーツを選択し終えたら準備完了にする
    }

    void ChangeBody()
    {
        if (ready) return; // 準備完了しているなら処理をさせない

        
        if (bodyList[bodyNumber - 1] != null) Destroy(bodyList[bodyNumber - 1]);
        // 前回選択していたオブジェクトを削除

        insectNumber++;
        // 虫の種類を変更
        if (insectNumber > 9) insectNumber = 1;
        // 虫の種類の選択できる範囲を超えたら、初期値に戻す
        slotTimer = 0;
        bodyList[bodyNumber - 1] = Instantiate((GameObject)Resources.Load("LoadPrefabs/" + selectCon.insectSelectCSVDatas[insectNumber][bodyNumber]),
                                   conectPoint[bodyNumber - 1]);
        // CSVから対応したオブジェクトを生成する

        bodyName[bodyNumber - 1] = insectNameList[insectNumber][bodyNumber];

        insectName.text = bodyName[0] + bodyName[1] + bodyName[2];

        

        if(bodyNumber == 2)
        {
            middleConect = bodyList[bodyNumber - 1].AddComponent<ReloadMiddleConectPoint>();

            middleConect.ReloadMiddle(conectPoint[0].gameObject, conectPoint[2].gameObject);
        }
    }
    // コントローラー用の関数
    public void InsectDecide(InputAction.CallbackContext context) 
    {
        if (cooltime) return;
        if (!context.performed) return;
        if (ready) return;

        bodyNumber += 1;
        // 次のパーツに進める
        insectNumber = 1;
        // 虫のナンバーを初期化する
        slotTimer = 0;
        // タイマーをリセット
        audioSource.PlayOneShot(selectSE);
        // 効果音を鳴らす

        cooltime = true;

        Instantiate(selectEff, bodyList[bodyNumber - 1].transform);
    }

    // PCでデバッグ用の関数 あんま使わない方がいいかも
    // 内容はコントローラーと同じ
    void DebugInsectDecide()
    {
        if (ready) return;

        bodyNumber++;
        insectNumber = 1;
        slotTimer = 0;
        audioSource.PlayOneShot(selectSE);

        Instantiate(selectEff, bodyList[bodyNumber - 1].transform);
    }
}
