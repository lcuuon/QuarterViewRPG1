using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    
    Rigidbody rb;

    public void Shoot(Quaternion direction)
    {
        rb = GetComponent<Rigidbody>();
        Invoke("DestroyBullet", 5f);
        Debug.Log(direction);
        rb.velocity = Vector3.forward * 10;
        //transform.LookAt(direction);
    }

    public void DestroyBullet()
    {
        ArrowPooling.ReturnObject(this);
    }

    void Update()
    {
    }
}
