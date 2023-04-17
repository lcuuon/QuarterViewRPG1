using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    
    Rigidbody rb;
    Vector3 direction_;
    Vector3 speed = Vector3.zero;

    public void Shoot(Vector3 direction, Vector3 rotate)
    {
        //transform.rotation = rotate;
        //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90 * (Mathf.PI / 180), 0);
        transform.rotation = Quaternion.Euler(new Vector3(90, rotate.y - 90, rotate.z));
        rb = GetComponent<Rigidbody>();
        Invoke("DestroyBullet", 5f);
        //transform.LookAt(new Vector3(direction.x, direction.y, direction.z));
        //rb.AddForce(direction * Time.deltaTime * 10, ForceMode.Impulse);
        //rb.velocity = new Vector3(-direction.x, direction.y, direction.z);
        //transform.LookAt(direction);
    }

    public void DestroyBullet()
    {
        ArrowPooling.ReturnObject(this);
    }

    void Update()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, direction_, ref speed, 0.5f);
        //rb.velocity = direction_ * 0.1f;
        transform.Translate(Vector3.up * Time.deltaTime * 10);
    }
}
