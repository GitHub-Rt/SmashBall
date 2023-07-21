using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private bool isStart; // �Q�[�����n�܂��Ă��邩�ǂ���
    public float elapsedTime;//�o�ߎ���
    public const float TIME_LIMIT = 30.0f;

    private int minute;   
    private float seconds;
    private float oldSeconds; //�O��Update�̎��̕b��
    void Start()
    {
        isStart = false;
        elapsedTime = 0f;
        minute = 0;
        seconds = 0f;
        oldSeconds = 0f;

        ///////////////////////////3�b���isStart��true�ɂ���//////////////////////
        Invoke(nameof(GameStart), 3.0f);
        Invoke(nameof(GameStart), 33.0f);
        Invoke(nameof(Finish), 33.0f);

    }

    // Update is called once per frame
    void Update()
    {

        //�����̓R�����g�A�E�g
        //if (Input.GetMouseButtonDown(0))
        //{
        //    isStart = true;
        //}

        
        TimerUpdate();

        //Debug.Log("�o�ߎ���(�b)" + Time.time);
    }

    //�^�C�}�[
    void TimerUpdate()
    {
        //�����A�Q�[�����n�܂��Ă�����
        if (isStart)
        {
            seconds += Time.deltaTime;
            if (seconds >= 60f)
            {
                minute++;
                seconds = seconds - 60;
            }
            //�@�l���ς�����������e�L�X�gUI���X�V
            if ((int)seconds != (int)oldSeconds)
            {
                elapsedTime = minute + ((int)seconds);
            }
            oldSeconds = seconds;
            timeText.text = (TIME_LIMIT - elapsedTime).ToString();
        }
       
    }

    //�Q�[�����J�n
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
