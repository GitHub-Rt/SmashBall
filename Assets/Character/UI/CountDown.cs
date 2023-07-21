using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro_;
    private int maxIndex_ = 0;
    private string[] count_ = { "3", "2", "1", "GO!","","Finish!" };
   
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro_ = GetComponent<TextMeshProUGUI>();
        InvokeRepeating(nameof(Count),0, 1.0f);
        Invoke(nameof(CancelInvoke), 5.0f);
    }

    public void Count()
    {
        textMeshPro_.text = count_[maxIndex_];
        maxIndex_++;
    }
}