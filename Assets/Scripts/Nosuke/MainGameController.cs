using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public GameObject PointParent;
    List<GameObject> points = new List<GameObject>();

    Dictionary<string,GameObject> pointsName = new Dictionary<string,GameObject>();
    private void Start()
    {
        foreach(Transform point in PointParent.transform)
        {
            points.Add(point.gameObject);
            pointsName.Add(point.name, point.gameObject);
            //Debug.Log(point.gameObject);
        }

        DecideThePoint();

        StartPointSet();

        GoalPointSet();
    }
    
    void DecideThePoint()
    {
        int start = Random.Range(0, points.Count);
        
        foreach(Transform startPoint in points[start].transform)
        {
            
        }
    }

    private void Update()
    {
        
    }

    void StartPointSet()
    {
        Transform startPoint = pointsName[PlayerPrefs.GetString("StartPoint")].transform;
        //Debug.Log(starPoint.gameObject.name);

        for(int i = 0; i <= 3; i++)
        {
            GameObject player = InsectSelectController.instance.playerList[i];

            player.transform.SetParent(startPoint.GetChild(i));
            player.transform.position = startPoint.GetChild(i).position;

            player.gameObject.AddComponent<Rigidbody>();
        }
    }

    void GoalPointSet()
    {
        Transform goalPoint = pointsName[PlayerPrefs.GetString("GoalPoint")].transform;
        Debug.Log(goalPoint.gameObject.name);

        goalPoint.gameObject.tag = "Goal";

        goalPoint.gameObject.GetComponent<EffectCreate>().EndEffect();

        goalPoint.gameObject.AddComponent<GoalScript>();
    }
}
