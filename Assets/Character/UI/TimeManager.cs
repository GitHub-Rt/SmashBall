using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private bool isStart; // ゲームが始まっているかどうか
    public float elapsedTime;//経過時間
    public const float TIME_LIMIT = 30.0f;

    private int minute;   
    private float seconds;
    private float oldSeconds; //前のUpdateの時の秒数
    void Start()
    {
        isStart = false;
        elapsedTime = 0f;
        minute = 0;
        seconds = 0f;
        oldSeconds = 0f;

        ///////////////////////////3秒後にisStartをtrueにする//////////////////////
        Invoke(nameof(GameStart), 3.0f);
        Invoke(nameof(GameStart), 33.0f);
        Invoke(nameof(Finish), 33.0f);

    }

    // Update is called once per frame
    void Update()
    {

        //ここはコメントアウト
        //if (Input.GetMouseButtonDown(0))
        //{
        //    isStart = true;
        //}

        
        TimerUpdate();

        //Debug.Log("経過時間(秒)" + Time.time);
    }

    //タイマー
    void TimerUpdate()
    {
        //もし、ゲームが始まっていたら
        if (isStart)
        {
            seconds += Time.deltaTime;
            if (seconds >= 60f)
            {
                minute++;
                seconds = seconds - 60;
            }
            //　値が変わった時だけテキストUIを更新
            if ((int)seconds != (int)oldSeconds)
            {
                elapsedTime = minute + ((int)seconds);
            }
            oldSeconds = seconds;
            timeText.text = (TIME_LIMIT - elapsedTime).ToString();
        }
       
    }

    //ゲームを開始
    void GameStart()
    {
        isStart = !isStart;
    }

    void Finish()
    {
        timeText.text = "";
        GameObject obj = GameObject.Find("Canvas").transform.Find("Count").gameObject;
        obj.GetComponent<CountDown>().Count();
        Invoke(nameof(GameEnd), 2.0f);
    }

    void GameEnd()
    {
        GetComponent<GameEnd>().Finish();
    }
}
