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
        // target�̈ړ��ʕ��A�����i�J�����j���ړ�����
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        

        // ��]��
        float inputHorizontal = 0;

        if (ballControll.state == PlayerControllerState.Attack)
        {
            // �{�^���𗣂������ǂ���
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
            // �J�����̉�](��{�͉E�W���C�X�e�B�b�N)
            inputHorizontal = Input.GetAxisRaw("Horizontal2");
        }
        


        // target�̈ʒu��Y���𒆐S�ɁA��]�i���]�j����
        transform.RotateAround(targetPos, Vector3.up, inputHorizontal * Time.deltaTime * 200f);

        // �J�����̐����ړ��i���p�x�����Ȃ��A�K�v��������΃R�����g�A�E�g�j
        //transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
    }
}