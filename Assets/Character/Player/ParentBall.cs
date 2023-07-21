using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParentBall : MonoBehaviour
{
    private GameObject ball;
    private GameObject parts;
    private Vector3[] partsPos;
    private bool isActive;
    public int countTime = 1;
    private float count;
    public int spawnRange = 10;

    // Start is called before the first frame update
    void Start()
    {
        parts = transform.GetChild(1).gameObject;
        parts.SetActive(false);
        ball = transform.GetChild(0).gameObject;
        isActive = true;
        count = (float)countTime;
        partsPos = new Vector3[parts.transform.childCount+1];
        int i = 0;
        foreach(Transform t in parts.GetComponentsInChildren<Transform>())
        {
            partsPos[i] = t.position;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == false)
        {
            if(count > 0)
            {
                count -= Time.deltaTime;
            }
            else
            {
                count = countTime;
                Respawn();
            }
        }
    }

    public void BreackBall()
    {
        if (ball.GetComponent<DamegeControl>().life_ <= 0)
        {
            string name = ball.GetComponent<DamegeControl>().GetCollisionObjectName();
            if (name != null)
            {
                GameObject obj = GameObject.Find(name);
                obj.GetComponent<KillCount>().AddCount();
                obj.GetComponent<DamegeControl>().ReSetCollisionObjectName();

            }
            Vector3 vec = ball.transform.position;

            ball.SetActive(false);
            parts.transform.position = vec;
            // Debug.Log(parts.gameObject.name);
            foreach (Rigidbody rigidbody in parts.GetComponentsInChildren<Rigidbody>())
            {
                //Debug.Log(parts.gameObject.name);
                rigidbody.AddExplosionForce(150f, ball.transform.position, 5f);
            }
            parts.SetActive(true);
            isActive = false;
        }
    }

    void Respawn()
    {
        ball.GetComponent<DamegeControl>().SetLife(100);
        ball.SetActive(true);
        parts.SetActive(false);
        var rand = new System.Random();
        float posX = rand.Next(-(spawnRange * 10), (spawnRange * 10) + 1) / 10;
        float posZ = rand.Next(-(spawnRange * 10), (spawnRange * 10) + 1) / 10;
        Vector3 vec = new Vector3(posX, 0, posZ);
        ball.transform.position = vec;
        isActive = true;
        int i = 0;
        foreach (Transform t in parts.GetComponentsInChildren<Transform>())
        {
            t.position = partsPos[i];
            i++;
        }

    }
}
