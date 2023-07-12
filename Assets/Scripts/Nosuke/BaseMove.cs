using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMove : MonoBehaviour
{
    Vector3 firstPos; // 初期位置

    float moveSpeed = 2; // 土台の動く速さ
    float amplitude = 0.1f; // 土台の動く幅の大きさ
    // 上二つはpublicにしてもいいかも

    float rotation; // 回転で使う値
    void Start()
    {
        // 初期位置を設定
        firstPos = transform.position;
    }
    void Update()
    {
        // 最初に設定した動く速さと動く幅に対して、土台をフワフワと動かしている
        transform.position = firstPos + new Vector3(0, Mathf.Sin(Time.time * moveSpeed) * amplitude, 0);

        rotation += Time.deltaTime * moveSpeed * 10; // 値をどんどん足していく

        // 回転させる
        transform.localEulerAngles = new Vector3(90, 0, rotation);
    }
}
