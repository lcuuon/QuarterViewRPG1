using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject ParentOb;
    private Rigidbody rb;
    private Animator anim;

    private NavMeshAgent nav;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        ParentOb.transform.position = transform.position;



        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {

                nav.SetDestination(new Vector3(hit.point.x, 1.8f, hit.point.z));

                if (hit.point.x == transform.position.x || hit.point.z == transform.position.z)
                {
                    anim.CrossFade("Idle", 0.3f);

                }
                else
                {
                    anim.CrossFade("Run", 0.3f);

                }
                //Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
}
