using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GoalScript : MonoBehaviour
{
    public List<GameObject> playerList = new List<GameObject>();
    public List<float> goalDistance = new List<float>();

    public MainGameController mainCon;

    Dictionary<float, GameObject> playerName = new Dictionary<float, GameObject>();

    public bool goal = false;
    

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

        mainCon.goalUI.SetActive(true);

        for(int i = 0; i <= 3; i++)
        {
            float f = (transform.position - playerList[i].transform.position).magnitude * -1;

            if (f < 0) f *= -1;
            Debug.Log(playerList[i]);
            Debug.Log(f);
            goalDistance.Add(f);

            playerName.Add(goalDistance[i], playerList[i]);

            playerList[i].GetComponent<InsectMove>().enabled = false;
            playerList[i].transform.GetChild(1).GetChild(0).gameObject.GetComponent<Test_Nos>().enabled = false;
        }

        goalDistance.Sort();

        for (int i = 0; i <= 3; i++)
        {
            Debug.Log((i + 1) + "ˆÊ;" + playerName[goalDistance[i]].name + " ‹——£:" + goalDistance[i]);

            KeepValue.keepValue.playInsectObj.Add(i + 1, playerName[goalDistance[i]]);

            DontDestroyOnLoad(playerName[goalDistance[i]]);

            playerName[goalDistance[i]].GetComponent<PlayerInput>().enabled = false;

            playerName[goalDistance[i]].GetComponent<Rigidbody>().isKinematic = true;
        }

        mainCon.mainAudioSource.PlayOneShot(mainCon.goalSE);
        mainCon.mainAudioSource.loop = false;

        Invoke("LoadRankingScene", 3);
    }

    void LoadRankingScene()
    {
        
        SceneManager.LoadScene(5);
    }
}
