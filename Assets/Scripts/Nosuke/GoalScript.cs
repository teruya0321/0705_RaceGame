using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public List<GameObject> playerList = new List<GameObject>();
    public List<float> goalDistance = new List<float>();

    public MainGameController mainCon;

    Dictionary<float, GameObject> playerName = new Dictionary<float, GameObject>();

    bool goal = false;

    private void Start()
    {
        for(int i = 1; i <= 4; i++)
        {
            playerList.Add(InsectSelectController.instance.dic["Player" + i]);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player") && !goal)
        {
            Goal();
        }
    }
    public void Goal()
    {
        goal = true;

        for(int i = 0; i <= 3; i++)
        {
            float f = (transform.position - playerList[i].transform.position).magnitude;

            Debug.Log(playerList[i]);
            Debug.Log(f);
            goalDistance.Add(f);

            playerName.Add(goalDistance[i], playerList[i]);

            playerList[i].GetComponent<InsectMove>().goal = true;
            playerList[i].transform.GetChild(1).GetChild(0).gameObject.GetComponent<Test_Nos>().goal = true;
        }

        goalDistance.Sort();

        for (int i = 0; i <= 3; i++)
        {
            Debug.Log((i + 1) + "ˆÊ;" + playerName[goalDistance[i]].name + " ‹——£:" + goalDistance[i]);

            KeepValue.keepValue.playInsectObj.Add(i + 1, playerName[goalDistance[i]]);
        }
    }
}
