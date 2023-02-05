using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    //Move
    public bool isAggro;
    public bool isAttack;
    private bool canMove;

    //Component
    public GameObject player;
    private NavMeshAgent nav;
    private Animator anim;
    Rigidbody rb;

    //Info
    public float HP;

    //CC
    private bool isknockback;


    void Start()
    {
        canMove = true;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canMove)
        {
            if (isAttack)
            {
                transform.LookAt(player.transform.position);
                anim.SetBool("isAttack", true);
                anim.SetBool("isRun", false);
                nav.SetDestination(transform.position);
            }
            else if (isAggro && !isAttack)
            {
                anim.SetBool("isRun", true);
                anim.SetBool("isAttack", false);
                nav.SetDestination(player.transform.position);
            }
            if (!isAggro && !isAttack)
            {
                anim.SetBool("isAttack", false);
                anim.SetBool("isRun", false);
                nav.SetDestination(transform.position);

            }
        }
        

        if (isknockback)
        {
            nav.SetDestination(transform.position);
            
        }
    }

    public void Knockback(Vector3 knockbackDir)
    {
        canMove = false;
        Debug.Log("Damage");
        isknockback = true;
        rb.velocity = knockbackDir;
        Invoke("KnockbackEnd", 0.2f);
        anim.SetBool("canMove", false);
        anim.CrossFade("Fall", 0f);
    }
    private void KnockbackEnd()
    {
        canMove = true;
        isknockback = false;
        rb.velocity = Vector3.zero;
        anim.SetBool("canMove", true);
    }
}
