using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class PlayerTagManager : MonoBehaviour
{
    [SerializeField]
    private Transform targetTfm;

    private UnityEngine.Vector3 myRectTfm;
    //private UnityEngine.Vector3 offset = new UnityEngine.Vector3(0, 1.5f, 0);

    public TextMeshProUGUI playerTagText;
    public TextMeshProUGUI enemy1TagText;
    public TextMeshProUGUI enemy2TagText;
    public TextMeshProUGUI enemy3TagText;

    public UnityEngine.Vector3 playerPos = UnityEngine.Vector3.zero;

    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;

    public GameObject player;

    private Camera mainCamera;

    const float SHIFT_UP_UI = 0.85f;
    const int CHARACTER_NUM = 4;

    // Start is called before the first frame update
    void Start()
    {
        //Playerオブジェクトを探す
        player = GameObject.Find("player");
        mainCamera = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {
        

        GameObject[] tagArray = {
            player,
            Enemy1,
            Enemy2,
            Enemy3
        };

        TextMeshProUGUI[] tagTextArray = {
           playerTagText,
           enemy1TagText,
           enemy2TagText,
           enemy3TagText
        };

        for (int i = 0; i < tagArray.Length; i++)
        {
            var center = 0.5f * new UnityEngine.Vector3(Screen.width, Screen.height);
            playerPos = tagArray[i].transform.position;
            playerPos.y += SHIFT_UP_UI;

            myRectTfm = mainCamera.WorldToScreenPoint(playerPos);

            // カメラ後方にあるターゲットのスクリーン座標は、画面中心に対する点対称の座標にする
            if (myRectTfm.z < 0.0f)
            {
                myRectTfm.x = -myRectTfm.x;
                myRectTfm.y = -myRectTfm.y;
            }

            tagTextArray[i].transform.position = myRectTfm;
        }
    }
        
}

