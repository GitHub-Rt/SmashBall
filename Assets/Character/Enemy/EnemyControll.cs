using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControll : MonoBehaviour
{
    private Rigidbody rb_;
    private GameObject target_;
    private float attackSpeed_;
    private string tag_;
    private float attackLimit_;
    NavMeshAgent agent_;
    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody>();
        agent_=GetComponent<NavMeshAgent>();
        tag_ = "Player";
        InvokeRepeating(nameof(Attack),3.0f, 5.0f);
        InvokeRepeating(nameof(Move),3.1f, 1.0f);
        InvokeRepeating(nameof(SetTarget),0, 4.0f);
        SetTarget();

        //メッシュエージェントを停止
        agent_.enabled = false;
        //メッシュエージェントを3秒後に起動
        Invoke(nameof(NavMeshAgentEnable), 3.0f);
        attackSpeed_ = 10;
        attackLimit_ = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SetTarget()
    {
        //周りの敵距離情報を集める
        List<GameObject> playerList = new List<GameObject>(GameObject.FindGameObjectsWithTag(tag_));
        int targetnum = Random.Range(0, playerList.Count - 1);
        target_= playerList[targetnum];
    }

    void Move()
    {
       // SetTarget();

        if (!agent_.pathPending)
        {
            Vector3 position = target_.transform.position;
            float x = Random.Range(-5, 5);
            float z = Random.Range(-5, 5);
            position.x += x;
            position.z += z;


            agent_.destination = position;
        }
    }

    void Attack()
    {
        Vector3 attackVec = target_.transform.position - this.transform.position;
       
        if(attackVec.magnitude<=attackLimit_)
        rb_.velocity += attackVec.normalized*attackSpeed_;

    }

    void NavMeshAgentEnable()
    {
        agent_.enabled = true;
    }
}
