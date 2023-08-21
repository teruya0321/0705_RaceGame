using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    List<GameObject> goalPlayer = new List<GameObject>();

    public void Goal(GameObject player)
    {
        goalPlayer.Add(player);
    }

    private void Update()
    {
        if(goalPlayer.Count == 4)
        {
            // ここにゴールした時の処理を書く。まだ話し合ってないので書いてない。ぴーすぴーす
        }
    }
}
