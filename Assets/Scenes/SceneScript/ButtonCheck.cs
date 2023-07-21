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
                // �v���C�V�[����
                SceneManager.LoadScene("SampleScene");
            }
            else if(collision.tag == "Operation")
            {
                // �v���C�V�[�����̑��������
                SceneManager.LoadScene("OperationScene");
            }
            else if(collision.tag == "ReturnTitle")
            {
                // �^�C�g���V�[����
                SceneManager.LoadScene("TitleScene");
            }
            else if(collision.tag == "NextOperation")
            {
                // �v���C�V�[���ȊO�̑��������
                SceneManager.LoadScene("OperationScene2");
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
