using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour
{
    public GameObject PointParent;
    List<GameObject> points = new List<GameObject>();

    Dictionary<string,GameObject> pointsName = new Dictionary<string,GameObject>();

    GameObject player;

    public List<GameObject> players = new List<GameObject>();

    Transform goalPoint;

    public Image countdownObj;

    public Sprite[] countdown;

    bool count  = false;

    List<float> playerRank = new List<float>();
    Dictionary<float,GameObject> playerRange = new Dictionary<float,GameObject>();
    public Image[] playerRankImage;
    public Sprite[] rankUI;
    private void Start()
    {
        foreach(Transform point in PointParent.transform)
        {
            points.Add(point.gameObject);
            pointsName.Add(point.name, point.gameObject);
            //Debug.Log(point.gameObject);
        }

        StartPointSet();

        GoalPointSet();
    }

    private void Update()
    {
        if (!count) return;

        for(int i = 1; i <= 4; i++)
        {
            float f = (goalPoint.position - players[i - 1].transform.position).magnitude;
            Debug.Log(f);

            playerRank.Add(f);

            playerRange.Add(f, players[i - 1]);
        }

        playerRank.Sort();
    }

    private void LateUpdate()
    {
        
    }

    void StartPointSet()
    {
        Transform startPoint = pointsName[PlayerPrefs.GetString("StartPoint")].transform;
        //Debug.Log(starPoint.gameObject.name);

        for(int i = 0; i <= 3; i++)
        {
            player = InsectSelectController.instance.playerList[i];

            player.transform.SetParent(startPoint.GetChild(i));
            player.transform.position = startPoint.GetChild(i).position;

            player.AddComponent<InsectMove>();

            players.Add(player);
        }

        StartCoroutine("GameStartCoroutine");
    }

    IEnumerator GameStartCoroutine()
    {
        countdownObj.sprite = countdown[0];

        yield return new WaitForSeconds(1);

        countdownObj.sprite = countdown[1];

        yield return new WaitForSeconds(1);

        countdownObj.sprite = countdown[2];

        yield return new WaitForSeconds(1);

        countdownObj.sprite = countdown[3];
        GameStart();

        yield return new WaitForSeconds(1);

        countdownObj.sprite = null;
        countdownObj.color = new Color(0.0f,0.0f, 0.0f, 0.0f);

        count = true;
    }
    void GameStart()
    {
        for (int i = 0; i <= 3; i++)
        {
            InsectSelectController.instance.playerList[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;

            InsectSelectController.instance.playerList[i].transform.GetChild(1).GetChild(0).gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void GoalPointSet()
    {
        goalPoint = pointsName[PlayerPrefs.GetString("GoalPoint")].transform;
        //Debug.Log(goalPoint.gameObject.name);

        goalPoint.gameObject.tag = "Goal";

        goalPoint.gameObject.GetComponent<EffectCreate>().EndEffect();

        goalPoint.gameObject.AddComponent<GoalScript>().mainCon = gameObject.GetComponent<MainGameController>();


    }
}
