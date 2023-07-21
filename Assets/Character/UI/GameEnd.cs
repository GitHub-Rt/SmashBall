using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    List<GameObject> players_;
    public static string[] nameList_ = new string[4];
    public static int[] killList_ = new int[4];
    
    // Start is called before the first frame update
    void Start()
    {
        players_ = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }

    public void Finish()
    {
        players_.Sort((a, b) => b.GetComponent<KillCount>().GetKillCount().CompareTo(a.GetComponent<KillCount>().GetKillCount()));
        for(int i=0; i<players_.Count; i++)
        {
            GameObject obj = players_[i];
            killList_[i]=obj.GetComponent<KillCount>().GetKillCount();
            nameList_[i]=obj.name;
        }
        SceneManager.LoadScene("ResultScene");
    }
}
