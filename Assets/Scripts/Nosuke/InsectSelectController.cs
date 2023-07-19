using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class InsectSelectController : MonoBehaviour
{
    public static InsectSelectController instance;

    TextAsset csvFile;
    public List<string[]> insectSelectCSVDatas = new List<string[]>();
    
    public Dictionary<string, GameObject> dic = new Dictionary<string, GameObject>();

    public List<GameObject> playerList = new List<GameObject>();

    private void Awake()
    {
        Debug.Log("test");

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //dic.Clear();

        Application.targetFrameRate = 60;
        csvFile = Resources.Load("CSVs/InsectSelectTest") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // àÍçsÇ∏Ç¬ì«Ç›çûÇ›
            insectSelectCSVDatas.Add(line.Split(',')); // , ãÊêÿÇËÇ≈ÉäÉXÉgÇ…í«â¡
        }

        dic.Add("Player1", null);
        dic.Add("Player2", null);
        dic.Add("Player3", null);
        dic.Add("Player4", null);
    }

    
    public void InsectList(GameObject player)
    {
        dic[player.name] = player;
        playerList.Add(player);
        if(playerList.Count == 4)
        {
            for(int i = 1;  i <= dic.Count; i++)
            {
                dic["Player" + i].gameObject.transform.parent = null;
                DontDestroyOnLoad(dic["Player" + i]);
                Destroy(dic["Player" + i].GetComponent<SelectInsect>());
            }
            SceneManager.LoadScene(1);
        }
    }


}
