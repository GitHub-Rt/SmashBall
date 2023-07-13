using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCheck : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Touch : " + collision.gameObject.name);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetButton("buttonA"))
        {
            if(collision.tag == "GameStart")
            {
                SceneManager.LoadScene("SampleScene");
            }
            else if(collision.tag == "Operation")
            {
                SceneManager.LoadScene("OperationScene");
            }
            else if(collision.tag == "ReturnTitle")
            {
                SceneManager.LoadScene("TitleScene");
            }
            else
            {
                Debug.Log("No! ChangeScene");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit : " + collision.gameObject.name);
    }

}
