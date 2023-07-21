using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    public int killCount_;
    // Start is called before the first frame update
    void Start()
    {
        killCount_ = 0;
    }

    public void AddCount()
    {
        killCount_++;
        Debug.Log(gameObject.name+killCount_.ToString());
    }

    public int GetKillCount()
    {
        return killCount_;
    }
}
