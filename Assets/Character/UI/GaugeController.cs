using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    [SerializeField] protected Image _gaugeImage;    //ゲージとして使うImage
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //ゲージの見た目を設定
    public void UpdateGauge(float current, float max)
    {
        _gaugeImage.fillAmount = current / max; //fillAmountを更新
    }
}
