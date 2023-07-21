using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.AI;

public class DamegeControl : MonoBehaviour
{
    public float life_;
    public float attack_;
    private float godTime_;
    private string collisionObjectName_= null;
    Rigidbody rb_;

    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody>();
        life_ = 100.0f;
        attack_ = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Ground")
            return;
        if (collision.gameObject == null)
        Debug.Log(collision.gameObject.name+":object not set");
        Vector3 targetVelocity = collision.gameObject.GetComponent<Rigidbody>().velocity;
        float targetLife = collision.gameObject.GetComponent<DamegeControl>().life_;
        float targetAttack = collision.gameObject.GetComponent<DamegeControl>().attack_;
        if (targetVelocity.magnitude < 3.0f)
            return;







        GameObject pParent = collision.transform.parent.gameObject;
        pParent.GetComponent<ParentBall>().BreackBall();
        targetLife -= targetAttack * Mathf.Abs(Mathf.Sin(Vector3.Dot(targetVelocity, rb_.velocity)));
        collision.gameObject.GetComponent<DamegeControl>().life_ = targetLife;

        collisionObjectName_ = collision.gameObject.name;
        Debug.Log(collision.gameObject.name + targetLife.ToString());

    }

    public void ReSetCollisionObjectName() 
    {
        collisionObjectName_ = null;
    }

    public string GetCollisionObjectName()
    {
        return collisionObjectName_;
    }

    public void SetLife(float life)
    {
        life_ = life;
    }
}
