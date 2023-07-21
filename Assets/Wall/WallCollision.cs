using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 50.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<DamegeControl>().SetLife(0);
            rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb.velocity.magnitude > speed)
            {
                GameObject pParent = collision.transform.parent.gameObject;
                Debug.Log(pParent.gameObject.name);
                pParent.GetComponent<ParentBall>().BreackBall();
            }
            Debug.Log("player‚É“–‚½‚Á‚½");
        }
    }
}
