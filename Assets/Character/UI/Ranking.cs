using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ranking : MonoBehaviour
{

    string[] namelist;
    int[] killlist;
    // Start is called before the first frame update
    void Start()
    {
        namelist = GameEnd.nameList_;
        killlist = GameEnd.killList_;
        GameObject.Find("FirstPlaceText").GetComponent<TextMeshProUGUI>().text = namelist[0] +" : "+ killlist[0].ToString();
        GameObject.Find("SecondPlaceText").GetComponent<TextMeshProUGUI>().text = namelist[1] +" : "+ killlist[1].ToString();
        GameObject.Find("ThirdPlaceText").GetComponent<TextMeshProUGUI>().text = namelist[2] +" : "+ killlist[2].ToString();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
