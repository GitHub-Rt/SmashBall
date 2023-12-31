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

    // 移動に関する変数
    public float moveSpeed = 1.5f;          // 移動スピード                    

    // 攻撃に関する変数
    private float powerMeter = 5f;                // パワーメーターの初期値
    public float Max_PowerMeter = 30f;            // パワーメーターの最大値
    public float powerMeterStep = 0.05f;           // パワーメーターの変動値
    private int timer = 0;                        // 現在時間
    private int MaxTime = 1200;                     // 攻撃フレーム数
    public bool isAttack = false;                // 攻撃を行ったかどうか

    public GameObject cameraObj;                // Main Cameraのオブジェクトを指定するようの変数
    public Rigidbody rb;                        // 物理的な移動等を行わせるための変数
    private Transform cameraTransform;          // Main CameraのTransform情報
    public PlayerControllerState state;        // プレイヤーの行動ステート

    [SerializeField] public GaugeController _gaugeController;  //使いたいゲージ操作クラス

    void Start()
    {
        // Main Cameraのオブジェクト情報を取得
        cameraObj = GameObject.Find("Main Camera");
        cameraTransform = cameraObj.transform;


        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        
        //コントロール可能になるまで3秒遅延させる
        Invoke(nameof(ControlEnable), 3.0f);


        GameObject tmp = GameObject.Find("PowerGauge");
        _gaugeController = tmp.GetComponent<GaugeController>();
    }

    void Update()
    {
        // 状態遷移にかかわる処理を行ったかどうか
        GetStateInput();

        // 各状態に応じた処理を行う
        switch (state)
        {
            case PlayerControllerState.Idle:
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
        // すでに攻撃状態だったら状態を変更させることができない
        if (state != PlayerControllerState.Attack)
        {
            // 攻撃状態に遷移するかどうか
            if (Input.GetButtonDown("buttonA") || Input.GetButtonDown("buttonB") || Input.GetButtonDown("buttonX") || Input.GetButtonDown("buttonY"))
            {
                // 状態を攻撃に変更
                //Debug.Log("state : Attack.");
                state = PlayerControllerState.Attack;

                return;
            }

            // 移動状態に遷移するかどうか
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            if (moveX != 0 || moveZ != 0)
            {
                // 状態を移動に変更
                //Debug.Log("state : Move.");
                state = PlayerControllerState.Move;
            }

        }
    }

    // 移動処理
    void MovePlayer()
    {
        // カメラの位置情報を取得
        cameraTransform = Camera.main.transform;

        // カメラの向きを基準にした正規化正面方向ベクトルを用意
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;

        // 移動ベクトル計算
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * moveSpeed;                // 前後
        Vector3 moveX = cameraTransform.right * Input.GetAxis("Horizontal") * moveSpeed;      // 左右

        // 入力が行われていなかったら移動状態をやめる
        if (moveX == Vector3.zero && moveZ == Vector3.zero)
        {
            // 状態を待機に遷移させる
            //Debug.Log("state : Idle.");
            state = PlayerControllerState.Idle;
            

            return;
        }

        // 移動ベクトル用意
        Vector3 moveDirection = moveZ + moveX;

        // 移動処理
        rb.velocity = moveDirection;
    }


    // 攻撃処理
    void AttackPlayer()
    {
        // 押している間パワーをためる
        if (Input.GetButton("buttonA") || Input.GetButton("buttonB") || Input.GetButton("buttonX") || Input.GetButton("buttonY"))
        {
            // 動きを停止させる
            rb.velocity = Vector3.zero;

            //
            powerMeter += powerMeterStep;

            // 最大値より上に行かないようにする
            if (powerMeter > Max_PowerMeter)
            {
                powerMeter = Max_PowerMeter;
            }
        }



        // ボタンを離したときにカメラのz軸方向に移動する
        if (Input.GetButtonUp("buttonA") || Input.GetButtonUp("buttonB") || Input.GetButtonUp("buttonX") || Input.GetButtonUp("buttonY"))
        {
            // パワーメーターの値を出力
            //Debug.Log("Now PowerMeter : " + powerMeter);


            // カメラの位置情報を取得
            cameraTransform = Camera.main.transform;

            // カメラの z 軸方向に移動
            Vector3 moveDirection = cameraTransform.forward;
            moveDirection.y = 0f;
            moveDirection.Normalize();
            moveDirection *= powerMeter;

            powerMeter = 0;

            // 攻撃処理
            rb.velocity = moveDirection;

            isAttack = true;

        }

        // 速度が一定以下になったら攻撃をやめる
        if (isAttack)
        {
            timer++;

            if (timer > MaxTime)
            {
                isAttack = false;
                timer = 0;

                // 状態を待機に変更
                //Debug.Log("state : Idle.");
                state = PlayerControllerState.Idle;
            }
        }

        //パワーゲージの初期値で更新
        _gaugeController.UpdateGauge(powerMeter, Max_PowerMeter);   //ゲージを初期値で更新

    }


    void ControlEnable()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

}

