using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{

    private Vector3 targetPos;
    public GameObject targetObj;
    public BallControll ballControll;

    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.Find("player");
        targetPos = targetObj.transform.position;
        ballControll = targetObj.GetComponent<BallControll>();
    }

    // Update is called once per frame
    void Update()
    {
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        

        // 回転量
        float inputHorizontal = 0;

        if (ballControll.state == PlayerControllerState.Attack)
        {
            // ボタンを離したかどうか
            if(ballControll.isAttack)
            {
                inputHorizontal = Input.GetAxisRaw("Horizontal2");
            }

            if(inputHorizontal == 0)
            {
                inputHorizontal = Input.GetAxisRaw("Horizontal");
            }
        }
        else
        {
            // カメラの回転(基本は右ジョイスティック)
            inputHorizontal = Input.GetAxisRaw("Horizontal2");
        }
        


        // targetの位置のY軸を中心に、回転（公転）する
        transform.RotateAround(targetPos, Vector3.up, inputHorizontal * Time.deltaTime * 200f);

        // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
        //transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
    }
}