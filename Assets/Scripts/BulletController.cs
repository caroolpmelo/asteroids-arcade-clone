using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    void Start()
    {
        // set bullet to destroy itself after 1s
        Destroy(gameObject, 1.0f);

        // push bullet to direction it's facing
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * 400);
    }
}
