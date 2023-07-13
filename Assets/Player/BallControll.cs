using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerControllerState
{
    Idle,
    Move,
    Attack
}


public class BallControll : MonoBehaviour
{
    // �ړ��Ɋւ���ϐ�
    public float moveSpeed = 1.5f;          // �ړ��X�s�[�h                    

    // �U���Ɋւ���ϐ�
    private float powerMeter = 5f;                // �p���[���[�^�[�̏����l
    public float Max_PowerMeter = 30f;            // �p���[���[�^�[�̍ő�l
    public float powerMeterStep = 0.3f;           // �p���[���[�^�[�̕ϓ��l
    private bool isAttack = false;              // �U�����s�������ǂ���


    public GameObject cameraObj;                // Main Camera�̃I�u�W�F�N�g���w�肷��悤�̕ϐ�
    public Rigidbody rb;                        // �����I�Ȉړ������s�킹�邽�߂̕ϐ�
    private Transform cameraTransform;          // Main Camera��Transform���
    private PlayerControllerState state;        // �v���C���[�̍s���X�e�[�g

    void Start()
    {
        // Main Camera�̃I�u�W�F�N�g�����擾
        cameraObj = GameObject.Find("Main Camera");
        cameraTransform = cameraObj.transform;


        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ��ԑJ�ڂɂ�����鏈�����s�������ǂ���
        GetStateInput();

        // �e��Ԃɉ������������s��
        switch(state)
        {
            case PlayerControllerState.Idle:
                Debug.Log("state : Idle.");
                break;
            case PlayerControllerState.Move:
                MovePlayer();
                break;
            case PlayerControllerState.Attack:
                AttackPlayer();
                break;
        }


        
    }

    
    void GetStateInput()
    {
        // ���łɍU����Ԃ��������Ԃ��U���ɂ͕ύX�����邱�Ƃ��ł��Ȃ�
        if(state != PlayerControllerState.Attack)
        {
            if (Input.GetButtonDown("buttonA") || Input.GetButtonDown("buttonB") || Input.GetButtonDown("buttonX") || Input.GetButtonDown("buttonY"))
            {
                // ��Ԃ��U���ɕύX
                Debug.Log("state : Attack.");
                state = PlayerControllerState.Attack;
            }
        }

        // �ړ���Ԃɂ͂ǂ�ȏ�Ԃ̎��ł��ύX�\�ɂ���


        float moveX = Input.GetAxis("Horizontal2");
        float moveZ = Input.GetAxis("Vertical2");

        if ( moveX != 0 || moveZ != 0 )
        {
            // ��Ԃ��ړ��ɕύX
            Debug.Log("state : Move.");
            state = PlayerControllerState.Move;
        }
    }


    // �ړ�����
    void MovePlayer()
    {
        // �J�����̈ʒu�����擾
        cameraTransform = Camera.main.transform;

        // �J�����̌�������ɂ������K�����ʕ����x�N�g����p��
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1,0,1)).normalized;

        // �ړ��x�N�g���v�Z
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical2") * moveSpeed;                // �O��
        Vector3 moveX = cameraTransform.right * Input.GetAxis("Horizontal2") * moveSpeed;     // ���E

        if( moveX == Vector3.zero && moveZ == Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }

        // �ړ��x�N�g���p��
        Vector3 moveDirection = moveZ + moveX;

        // �ړ�����
        rb.velocity = moveDirection;

        if (rb.velocity == Vector3.zero)
        {
            // ��Ԃ�ҋ@�ɑJ�ڂ�����
            state = PlayerControllerState.Idle;
        }
    }


    // �U������
    void AttackPlayer()
    {
        // �����Ă���ԃp���[�����߂�
        if (Input.GetButton("buttonA") || Input.GetButton("buttonB") || Input.GetButton("buttonX") || Input.GetButton("buttonY"))
        {
            powerMeter += powerMeterStep;

            // �ő�l����ɍs���Ȃ��悤�ɂ���
            if (powerMeter > Max_PowerMeter)
            {
                powerMeter = Max_PowerMeter;
            }
        }



        // �{�^���𗣂����Ƃ��ɃJ������z�������Ɉړ�����
        if (Input.GetButtonUp("buttonA") || Input.GetButtonUp("buttonB") || Input.GetButtonUp("buttonX") || Input.GetButtonUp("buttonY"))
        {
            // �p���[���[�^�[�̒l���o��
            Debug.Log("Now PowerMeter : " + powerMeter);


            // �J�����̈ʒu�����擾
            cameraTransform = Camera.main.transform;

            // �J������ z �������Ɉړ�
            Vector3 moveDirection = cameraTransform.forward;
            moveDirection.y = 0f;
            moveDirection.Normalize();
            moveDirection *= powerMeter;


            // �U������
            rb.velocity = moveDirection;

            isAttack = true;

        }

        // ���x�����ȉ��ɂȂ�����U������߂�
        if (rb.velocity..magnitude < 10.0f) 
        {
            rb.velocity = Vector3.zero;

            // ��Ԃ�ҋ@�ɕύX
            state = PlayerControllerState.Idle;
        }

    }

}

