using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInsect : MonoBehaviour
{
    public Transform[] bodyparts;
    // それぞれの部位を置く空オブジェクトの場所
    GameObject[] bodyList;
    // 現在表示されているボディパーツ

    int bodyNumber;
    // 現在選択している部位
    int insectNumber;
    // 現在選択している虫の種類

    void Update()
    {
        if(Input.GetAxisRaw("Vertical") > 0) ChangeBody(1);
        else if(Input.GetAxisRaw("Vertical") < 0) ChangeBody(-1);
        // パーツの種類を変える

        if (Input.GetAxisRaw("Horizontal") > 0) bodyNumber++;
        else if (Input.GetAxisRaw("Horizontal") < 0) bodyNumber--;
        // 選択している部位を変える

        if(bodyNumber > bodyparts.Length) bodyNumber = 0;
        else if(bodyNumber < 0) bodyNumber = bodyparts.Length;
        // パーツが選択できる範囲を超えたら、それぞれ最小値、最大値にする
    }

    void ChangeBody(int i)
    {
        Destroy(bodyList[bodyNumber]);
        // 前回選択していたオブジェクトを削除

        insectNumber += i;
        // 虫の種類を変更
        if (insectNumber > 9) insectNumber = 1;
        else if (insectNumber < 0) insectNumber = 9;
        // 虫の種類が選択できる範囲を超えたら、それぞれ最小値、最大値にする

        bodyList[bodyNumber] = Instantiate((GameObject)Resources.Load("LoadPrefabs/"), bodyparts[bodyNumber].position, Quaternion.identity, transform);
        // CSVから対応したオブジェクトを生成する
    }
}
