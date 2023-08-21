using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadMapController : MonoBehaviour
{
    public Transform pointParent;

    List<Transform> points = new List<Transform>();

    public List<int> numbers = new List<int>();
    // Start is called before the first frame update

    private void Awake()
    {
        foreach (Transform point in pointParent.transform)
        {
            points.Add(point);
        }

        for (int i = 0; i <= points.Count; i++)
        {
            numbers.Add(i);
        }

    }
    void Start()
    {
        int startI = Random.Range(0, numbers.Count - 1);
        //Debug.Log(numbers.Count - 1);
        int startNum = numbers[startI];
        numbers.RemoveAt(startI);
        //Debug.Log(startI);

        GameObject start = points[startNum].gameObject;
        PlayerPrefs.SetString("StartPoint",start.name);
        Debug.Log(start);

        int endI = Random.Range(0, numbers.Count - 1);
        //Debug.Log(numbers.Count - 1);
        int endNum = numbers[endI];
        numbers.RemoveAt(endI);
        //Debug.Log(endI);

        GameObject end = points[endNum].gameObject;
        PlayerPrefs.SetString("GoalPoint", end.name);
        Debug.Log(end);

        StartCoroutine(LoadNextScene(start, end));
    }

    IEnumerator LoadNextScene(GameObject startPoint,GameObject endPoint)
    {
        startPoint.GetComponent<EffectCreate>().StartEffect();
        endPoint.GetComponent<EffectCreate>().EndEffect();
        //Debug.Log(startPoint.GetComponent<EffectCreate>());

        yield return new WaitForSeconds(10);

        SceneManager.LoadScene(2);
    }
}
