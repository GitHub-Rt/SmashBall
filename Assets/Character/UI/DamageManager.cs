using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class DamageManager : MonoBehaviour
{
    public float damage = 100;
    public TextMeshProUGUI damageText;
    private DamegeControl damegeControl;
    private GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<UnityEngine.UI.Text>().text = damage.ToString();
        //damageText = GameObject.Find("DamageText").GetComponent<Text>();
        playerObj = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        damageText.text = damage.ToString();
        damageText.color = Color.red;

        
        damegeControl = playerObj.GetComponent<DamegeControl>();
        damage = (int)(100.0f - damegeControl.life_);
    }

    float GetDamage()
    {
        return damage;
    }
}
